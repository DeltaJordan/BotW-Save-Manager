using System.Buffers.Binary;
using System.Text;
using BotwSaveManager.Core;
using Xunit;

namespace BotwSaveManager.Core.Tests;

public class StringConversionTests
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ConvertPlatformPreservesSavedMapName(bool fromWiiU)
    {
        AssertStringConversion(0x0BEE9E46, "Z-9", 32, fromWiiU);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ConvertPlatformPreservesWolfLinkDetectedUid(bool fromWiiU)
    {
        AssertStringConversion(0x1C10782F, "test-wolf-uid-0123456789", 64, fromWiiU);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ConvertPlatformStillPreservesHorseNames(bool fromWiiU)
    {
        AssertStringConversion(0x7B74E117, "Sage", 64, fromWiiU);
    }

    private static void AssertStringConversion(uint hash, string text, int capacity, bool fromWiiU)
    {
        // All input is generated here; no player saves or game assets are needed.
        string root = Path.Combine(Path.GetTempPath(), "BotwSaveManager.Tests-" + Guid.NewGuid());
        string source = Path.Combine(root, "source");
        string output = Path.Combine(root, "output");
        string roundTrip = Path.Combine(root, "round-trip");
        Directory.CreateDirectory(Path.Combine(source, "0"));

        try {
            byte[] options = BuildOptions(fromWiiU);
            byte[] original = BuildSave(hash, text, capacity, fromWiiU);
            byte[] expected = BuildSave(hash, text, capacity, !fromWiiU);
            byte[] photo = { 0xFF, 0xD8, 0xFF, 0xD9 };
            File.WriteAllBytes(Path.Combine(source, "option.sav"), options);
            File.WriteAllBytes(Path.Combine(source, "0", "game_data.sav"), original);
            File.WriteAllBytes(Path.Combine(source, "0", "caption.jpg"), photo);

            BotwSave save = new(source);
            Assert.Equal(fromWiiU ? SaveType.WiiU : SaveType.Switch, save.SaveType);
            save.ConvertPlatform(output);

            // A round trip alone would also pass with the old, corrupting converter.
            // Check the independently generated destination byte order on the first pass.
            Assert.Equal(expected, File.ReadAllBytes(Path.Combine(output, "0", "game_data.sav")));
            Assert.Equal(BuildOptions(!fromWiiU), File.ReadAllBytes(Path.Combine(output, "option.sav")));
            Assert.Equal(fromWiiU ? SaveType.Switch : SaveType.WiiU, save.SaveType);
            Assert.Equal(original, File.ReadAllBytes(Path.Combine(source, "0", "game_data.sav")));
            Assert.Equal(options, File.ReadAllBytes(Path.Combine(source, "option.sav")));
            Assert.Equal(photo, File.ReadAllBytes(Path.Combine(output, "0", "caption.jpg")));

            BotwSave converted = new(output);
            converted.ConvertPlatform(roundTrip);
            Assert.Equal(original, File.ReadAllBytes(Path.Combine(roundTrip, "0", "game_data.sav")));
            Assert.Equal(options, File.ReadAllBytes(Path.Combine(roundTrip, "option.sav")));
        }
        finally {
            Directory.Delete(root, true);
        }
    }

    private static byte[] BuildOptions(bool bigEndian)
    {
        using MemoryStream stream = new();
        WriteHeader(stream, bigEndian);
        WriteWord(stream, 0, bigEndian);
        return stream.ToArray();
    }

    private static byte[] BuildSave(uint hash, string text, int capacity, bool bigEndian)
    {
        using MemoryStream stream = new();
        WriteHeader(stream, bigEndian);

        // Numeric entries before and after the string must still change byte order.
        WriteWord(stream, 0x10203040, bigEndian);
        WriteWord(stream, 0x12345678, bigEndian);
        byte[] value = new byte[capacity];
        Encoding.UTF8.GetBytes(text).CopyTo(value, 0);
        for (int i = 0; i < value.Length; i += 4) {
            WriteWord(stream, hash, bigEndian);
            stream.Write(value, i, 4);
        }
        WriteWord(stream, 0x50607080, bigEndian);
        WriteWord(stream, 0x87654321, bigEndian);
        WriteWord(stream, 0, bigEndian);
        return stream.ToArray();
    }

    private static void WriteHeader(Stream stream, bool bigEndian)
    {
        WriteWord(stream, 0x471B, bigEndian);
        WriteWord(stream, 0xFFFFFFFF, bigEndian);
        WriteWord(stream, 1, bigEndian);
    }

    private static void WriteWord(Stream stream, uint value, bool bigEndian)
    {
        Span<byte> bytes = stackalloc byte[4];
        if (bigEndian) {
            BinaryPrimitives.WriteUInt32BigEndian(bytes, value);
        }
        else {
            BinaryPrimitives.WriteUInt32LittleEndian(bytes, value);
        }
        stream.Write(bytes);
    }
}
