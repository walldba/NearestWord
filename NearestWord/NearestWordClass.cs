using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Fastenshtein;

namespace NearestWord
{
    public static class NearestWordClass
    {
        /// <summary>
        /// Checks distance between a string and a list of synonyms
        /// </summary>
        /// <param name="typedWord">Text to be treated and compared</param>
        /// <param name="synonyms">List of synonyms to be compared</param>
        /// <param name="regexPattern">Stopwords that will be removed in Regex</param>
        /// <param name="maxChanges">Maximum accepted changes</param>
        /// <returns>boolean and which treated string exists in the synonym list</returns>
        public static (bool, string) ApproximateWord(this string typedWord, IEnumerable<string> synonyms,
            IEnumerable<string> regexPattern, int maxChanges)
        {
            var wordSuggestion = ReplaceRegex(typedWord, CreateRegexPattern(regexPattern))
                                 .RemoveSpecialCharacters()
                                 .RemoveWhiteSpaces()
                                 .LevenshteinDistance(synonyms, maxChanges);

            return (wordSuggestion != null, wordSuggestion);
        }

        /// <summary>
        /// Remove stop words passed as parameter
        /// </summary>
        /// <param name="typedWord">Text to be treated and compared</param>
        /// <param name="regexPattern">Stopwords that will be removed in Regex</param>
        /// <returns>Return string without stop words</returns>
        public static string ReplaceRegex(this string typedWord, string regexPattern)
            => Regex.Replace(typedWord, regexPattern, string.Empty, RegexOptions.IgnoreCase);

        /// <summary>
        /// Treated string for regex pattern
        /// </summary>
        /// <param name="regexPattern">Stopwords that will be removed in Regex</param>
        /// <returns>Return treated string for regex pattern</returns>
        public static string CreateRegexPattern(this IEnumerable<string> regexPattern)
            => string.Join(" | ", regexPattern.Select(x => x.ToString().Trim()));

        /// <summary>
        /// Remove special characters
        /// </summary>
        /// <param name="typedWord">Text to be treated</param>
        /// <returns>String without special characters</returns>
        public static string RemoveSpecialCharacters(this string typedWord)
        {
            const string withSpecial = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç!@#$%¨&*()_+-={[}]~^/?;:.>,<\\|'\"";
            const string withoutSpecial = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc                                  ";

            for (int i = 0; i < withSpecial.Length; i++)
                typedWord = typedWord.Replace(withSpecial[i].ToString(), withoutSpecial[i].ToString());

            return Regex.Replace(typedWord, @"\s{2,}", " ").Trim();
        }

        /// <summary>
        /// Remove whitespaces
        /// </summary>
        /// <param name="typedWord">Text to be treated</param>
        /// <returns>String without whitespaces</returns>
        public static string RemoveWhiteSpaces(this string typedWord)
            => Regex.Replace(typedWord, @"\s+", string.Empty);

        /// <summary>
        /// Checks whether the number of changes between the word entered and the synonym list
        ///is less than or equal to the maximum of changes passed as parameter
        /// </summary>
        /// <param name="typedWord">Word that will be compared with synonym</param>
        /// <param name="synonyms">List of synonyms to be compared</param>
        /// <param name="maxChanges">Maximum accepted changes</param>
        /// <returns>returns string that is equivalent to the list of synonyms and meets the maximum of changes</returns>
        public static string LevenshteinDistance(this string typedWord, IEnumerable<string> synonyms,
            int maxChanges)
            => synonyms.FirstOrDefault(x => new Levenshtein(typedWord)
                        .DistanceFrom(x.Replace(" ", string.Empty)) <= maxChanges);
    }
}

