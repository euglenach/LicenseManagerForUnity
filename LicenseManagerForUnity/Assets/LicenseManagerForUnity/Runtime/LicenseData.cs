using UnityEngine;

namespace LicenseManagerForUnity
{
    [CreateAssetMenu(fileName = nameof(LicenseData), menuName = "LicenseManagerForUnity/LicenseData", order = 0)]
    public class LicenseData : ScriptableObject
    {
        [SerializeField] private string libraryName;
        [SerializeField] private TextAsset licenseTextFile;
        [SerializeField, TextArea(5, 25)] private string licenseRawText;
        
        public string LibraryName => libraryName;
        
        public string GetLicenseText()
        {
            if (licenseTextFile != null && licenseTextFile.text.Length > 0)
            {
                return licenseTextFile.text;
            }
            else
            {
                return licenseRawText;
            }
        }
    }
}
