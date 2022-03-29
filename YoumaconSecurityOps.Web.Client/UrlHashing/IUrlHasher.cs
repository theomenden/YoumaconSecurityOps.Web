namespace YoumaconSecurityOps.Web.Client.UrlHashing;

/// <summary>
/// Defines signatures for methods to encode and decode urls via a simple hashing algorithm
/// </summary>
public interface IUrlHasher
{
    #region Decoding Methods
    Int32 DecodeSingleValue(String hash);

    Int64 DecodeSingleLong(String hash);

    (Boolean isSuccessful, Int32 value) TryDecodeSingleValue(String hash);

    (Boolean isSuccessful, Int64 value) TryDecodeSingleLong(string hash);

    Int32[] DecodeValuesAsIntegers(String hash);

    Int64[] DecodeValuesAsLongs(String hash);

    (Boolean isSuccessful, Int64 value) TryDecodeValuesAsLongs(String hash);

    String DecodeHexValue(String hash);
    #endregion
    #region Integer Encoding Methods

    String EncodeInteger(Int32 number);

    String EncodeIntegers(IEnumerable<Int32> numbers);

    String EncodeIntegers(params Int32[] numbers);
    #endregion
    #region Long Encoding Methods
    String EncodeLong(Int64 number);

    String EncodeLongs(IEnumerable<Int64> numbers);

    String EncodeLongs(params Int64[] numbers);
    #endregion
    #region String Encoding Methods

    String EncodeHex(string hex);

    #endregion
}

