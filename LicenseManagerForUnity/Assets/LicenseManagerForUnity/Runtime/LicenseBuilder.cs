using System.Text;
using UnityEngine;

namespace LicenseManagerForUnity
{
    [CreateAssetMenu(fileName = nameof(LicenseBuilder), menuName = "LicenseManagerForUnity/LicenseBuilder", order = 0)]
    public class LicenseBuilder : ScriptableObject
    {
        [SerializeField] private LicenseData[] licenseList;
        [SerializeField] private string libraryNameBracketStart, libraryNameBracketEnd;
        [SerializeField] private string librarySeparator;
        private readonly StringBuilder textBuilder = new();

        public string Build()
        {
            textBuilder.Clear();
            
            for(var i = 0; i < licenseList.Length; i++)
            {
                var licenseData = licenseList[i];
                textBuilder.Append(libraryNameBracketStart);
                textBuilder.Append(licenseData.LibraryName);
                textBuilder.AppendLine(libraryNameBracketEnd);
                textBuilder.Append(licenseData.GetLicenseText());
                if(i < licenseList.Length - 1)
                {
                    textBuilder.AppendLine();
                    textBuilder.AppendLine(librarySeparator);
                    textBuilder.AppendLine();
                }
            }

            return textBuilder.ToString();
        }
        
#if UNITY_EDITOR
        
#endif
    }
}
