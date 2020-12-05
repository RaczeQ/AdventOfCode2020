using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day4
{
    public class Passport
    {
        private delegate bool ValidateField(string value);

        private static readonly List<string> EclValidValues = new() {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};

        private static readonly IDictionary<string, ValidateField> RequiredFields =
            new Dictionary<string, ValidateField>()
            {
                ["byr"] = ValidateByr,
                ["iyr"] = ValidateIyr,
                ["eyr"] = ValidateEyr,
                ["hgt"] = ValidateHgt,
                ["hcl"] = ValidateHcl,
                ["ecl"] = ValidateEcl,
                ["pid"] = ValidatePid,
            };

        private readonly string _passportContent;
        private readonly IDictionary<string, string> _passportContentPieces;

        public Passport(string content)
        {
            _passportContent = content;
            _passportContentPieces = _passportContent
                .Split(" ")
                .ToDictionary(k => k.Split(":")[0], v => v.Split(":")[1]);
        }

        public bool IsValid => RequiredFields.Keys.All(f => _passportContent.Contains($"{f}:"));

        public bool IsValidStrict => RequiredFields.All(kv =>
            _passportContent.Contains($"{kv.Key}:") && kv.Value(_passportContentPieces[kv.Key]));

        private static bool ValidateByr(string input)
        {
            return input.Length == 4 && int.TryParse(input, out int y) && y >= 1920 && y <= 2002;
        }

        private static bool ValidateIyr(string input)
        {
            return input.Length == 4 && int.TryParse(input, out int y) && y >= 2010 && y <= 2020;
        }

        private static bool ValidateEyr(string input)
        {
            return input.Length == 4 && int.TryParse(input, out int y) && y >= 2020 && y <= 2030;
        }

        private static bool ValidateHgt(string input)
        {
            return (input.EndsWith("cm") && int.TryParse(input.Replace("cm", ""), out int hgtCm) && hgtCm >= 150 &&
                    hgtCm <= 193)
                   || (input.EndsWith("in") && int.TryParse(input.Replace("in", ""), out int hgtIn) && hgtIn >= 59 &&
                       hgtIn <= 76);
        }

        private static bool ValidateHcl(string input)
        {
            return input.StartsWith("#") && int.TryParse(input.Replace("#", ""),
                System.Globalization.NumberStyles.HexNumber,
                System.Globalization.CultureInfo.InvariantCulture, out int hex);
        }

        private static bool ValidateEcl(string input)
        {
            return EclValidValues.Contains(input);
        }

        private static bool ValidatePid(string input)
        {
            return input.Length == 9 && new Regex(@"\d{9}", RegexOptions.Compiled).IsMatch(input);
        }
    }
}