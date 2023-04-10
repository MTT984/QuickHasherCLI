using System;
using System.Linq;
using System.Text;

namespace QuickHasherCLI
{
    class Hasher
    {
        public static uint BinHash(string k)
        {
            var hash = 0xFFFFFFFFu;
            byte[] bytes = Encoding.GetEncoding(1252).GetBytes(k);

            for (int i = 0; i < k.Length; i++)
            {
                hash = bytes[i] + 33 * hash;
            }

            return hash;
        }

        public static string CommerceHash(string hashIn)
        {
            hashIn = HashConvert(hashIn, "bin_mem").ToLower();
            string hash = $"0x{string.Join("", BitConverter.GetBytes(VLT32Hash(hashIn)).Reverse().Select(c => c.ToString("x2")))}";
            return hash;
        }

        public static string HashConvert(string hashInput, string hashType)
        {
            string hashConverted = string.Empty;
            uint tmp_hashConverted;

            byte[] bin_bytes = BitConverter.GetBytes(BinHash(hashInput));
            byte[] vlt32_bytes = BitConverter.GetBytes(VLT32Hash(hashInput));

            switch (hashType)
            {
                case "bin_mem_int":
                    hashConverted = BinHash(hashInput.ToUpper()).ToString();
                    break;
                case "bin_mem_uint":
                    tmp_hashConverted = BinHash(hashInput.ToUpper());
                    hashConverted = ((Int32)tmp_hashConverted).ToString();
                    break;
                case "vlt_mem_int":
                    hashConverted = VLT32Hash(hashInput.ToLower()).ToString();
                    break;
                case "vlt_mem_uint":
                    tmp_hashConverted = VLT32Hash(hashInput.ToLower());
                    hashConverted = ((Int32)tmp_hashConverted).ToString();
                    break;
                case "vlt_file":
                    hashConverted = $"0x{string.Join("", vlt32_bytes.Select(c => c.ToString("X2")))}";
                    break;
                case "vlt_mem":
                    hashConverted = $"0x{string.Join("", vlt32_bytes.Reverse().Select(c => c.ToString("x2")))}";
                    break;
                case "bin_mem":
                    hashConverted = $"0x{string.Join("", bin_bytes.Reverse().Select(c => c.ToString("x2")))}";
                    break;
                case "bin_file":
                    hashConverted = $"0x{string.Join("", bin_bytes.Select(c => c.ToString("X2")))}";
                    break;
                default:
                    break;
            }

            bin_bytes = null; vlt32_bytes = null;

            return hashConverted;
        }

        public static uint VLT32Hash(string k, uint init = 0xABCDEF00)
        {
            int koffs = 0;
            int len = k.Length;
            uint a = 0x9e3779b9;
            uint b = a;
            uint c = init;

            while (len >= 12)
            {
                a += (uint)k[0 + koffs] + ((uint)k[1 + koffs] << 8) + ((uint)k[2 + koffs] << 16) + ((uint)k[3 + koffs] << 24);
                b += (uint)k[4 + koffs] + ((uint)k[5 + koffs] << 8) + ((uint)k[6 + koffs] << 16) + ((uint)k[7 + koffs] << 24);
                c += (uint)k[8 + koffs] + ((uint)k[9 + koffs] << 8) + ((uint)k[10 + koffs] << 16) + ((uint)k[11 + koffs] << 24);

                a -= b; a -= c; a ^= (c >> 13);
                b -= c; b -= a; b ^= (a << 8);
                c -= a; c -= b; c ^= (b >> 13);
                a -= b; a -= c; a ^= (c >> 12);
                b -= c; b -= a; b ^= (a << 16);
                c -= a; c -= b; c ^= (b >> 5);
                a -= b; a -= c; a ^= (c >> 3);
                b -= c; b -= a; b ^= (a << 10);
                c -= a; c -= b; c ^= (b >> 15);

                koffs += 12;
                len -= 12;
            }

            c += (uint)k.Length;

            switch (len)
            {
                case 11:
                    c += (uint)k[10 + koffs] << 24;
                    goto case 10;
                case 10:
                    c += (uint)k[9 + koffs] << 16;
                    goto case 9;
                case 9:
                    c += (uint)k[8 + koffs] << 8;
                    goto case 8;
                case 8:
                    b += (uint)k[7 + koffs] << 24;
                    goto case 7;
                case 7:
                    b += (uint)k[6 + koffs] << 16;
                    goto case 6;
                case 6:
                    b += (uint)k[5 + koffs] << 8;
                    goto case 5;
                case 5:
                    b += (uint)k[4 + koffs];
                    goto case 4;
                case 4:
                    a += (uint)k[3 + koffs] << 24;
                    goto case 3;
                case 3:
                    a += (uint)k[2 + koffs] << 16;
                    goto case 2;
                case 2:
                    a += (uint)k[1 + koffs] << 8;
                    goto case 1;
                case 1:
                    a += (uint)k[0 + koffs];
                    break;
            }

            a -= b; a -= c; a ^= (c >> 13);
            b -= c; b -= a; b ^= (a << 8);
            c -= a; c -= b; c ^= (b >> 13);
            a -= b; a -= c; a ^= (c >> 12);
            b -= c; b -= a; b ^= (a << 16);
            c -= a; c -= b; c ^= (b >> 5);
            a -= b; a -= c; a ^= (c >> 3);
            b -= c; b -= a; b ^= (a << 10);
            c -= a; c -= b; c ^= (b >> 15);

            return c;
        }
    }
}