using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Way2.Infrastructure.Persistence.Conventions
{
    /// <summary>
    /// 
    /// </summary>
    public class SnakeCaseNameTranslator
    {

        public string TranslateTypeName(IMutableEntityType entityType)
        {
            var name = entityType.GetTableName();
            return ConvertToSnakeCaseName(name);
        }

        public string TranslateMemberName(IMutableProperty property)
        {

            string name;
            if (property.IsPrimaryKey())
            {
                var entityName = property.FindContainingPrimaryKey().DeclaringEntityType.ClrType.Name;
                name = entityName + "Id";
            }
            else
            {
                name = property.GetColumnName();
                if (property.ClrType == typeof(bool) && name.StartsWith("Is"))
                {
                    name = name.Remove(0, 2);
                }
            }
            return ConvertToSnakeCaseName(name);

        }

        private static string ConvertToSnakeCaseName(string name)
        {
            return Regex.Replace(name, @"(?<=[a-z])([A-Z])", "_$1").ToLower();
        }

    }

}
