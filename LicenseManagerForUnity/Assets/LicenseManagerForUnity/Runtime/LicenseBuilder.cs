using System.IO;
using System.Text;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace LicenseManagerForUnity
{
    [CreateAssetMenu(fileName = nameof(LicenseBuilder), menuName = "LicenseManagerForUnity/LicenseBuilder", order = 0)]
    public class LicenseBuilder : ScriptableObject
    {
        [SerializeField] private LicenseData[] licenseList;
        [SerializeField] private string libraryNameBracketStart, libraryNameBracketEnd;
        [SerializeField] private string librarySeparator;
        [SerializeField] private TextAsset outputFile;
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
        
        public void ExportToFile(TextAsset file)
        {
            if(file == null) return;
            
            var textPath = AssetDatabase.GetAssetPath(file);
            if(string.IsNullOrEmpty(textPath)) return;
            
            File.WriteAllText(textPath, Build());
            
            AssetDatabase.ImportAsset(textPath);
            AssetDatabase.Refresh();
        }
        
#if UNITY_EDITOR
        
#endif
    }
}
