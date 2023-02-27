using backend.Interfaces;

namespace riesgos_backend.utils
{
    public class BCryptImp : ICrypto
    {
        public bool verify(string data, string dataToCompare)
        {
            return BCrypt.Net.BCrypt.Verify(data, dataToCompare);
        }

        public string encrypt(string data)
        {
            return BCrypt.Net.BCrypt.HashPassword(data);
        }
    }
}
