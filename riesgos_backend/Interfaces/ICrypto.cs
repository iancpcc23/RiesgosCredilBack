namespace backend.Interfaces
{
    public interface ICrypto
    {
        public string encrypt(string data);
        public bool verify(string data, string dataToCompare);
    }
}
