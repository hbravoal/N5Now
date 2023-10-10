namespace N5.User.Application.Rules.Common
{
    /// <summary>
    /// Class to do additional validations that are not in the core of FluentValidation.
    /// </summary>
    public static class SpecialValidation
    {
        /// <summary>
        /// Method that validates if a given Guid is valid and not empty.
        /// </summary>
        /// <param name="strGuid">Guide to validate.</param>
        /// <returns>Boolean that indicates if it is valid (True).</returns>
        public static bool BeValidGuid(string strGuid)
        {
            Guid guid;
            bool isValid = Guid.TryParse(strGuid, out guid);

            return isValid && guid != Guid.Empty;
        }
    }
}
