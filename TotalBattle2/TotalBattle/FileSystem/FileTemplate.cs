using System;

namespace TotalBattle.FileSystem
{
    public abstract class FileTemplate
    {
        protected const char DataSplitter = ';';
        protected abstract string filePath { get; }

        public void Save() => FileManager.WriteToFile(GetData(), filePath);
        public void Load() => SetData(GetData());

        public void Delete() => FileManager.DeleteFile(filePath);
        
        protected abstract string[] GetData();
        protected abstract void SetData(string[] data);

        protected int ParseToInt(string value)
        {
            try
            {
                return int.Parse(value);
            }
            catch (SystemException)
            {
                return 0;
            }
        }

        protected float ParseToFloat(string value)
        {
            try
            {
                return float.Parse(value);
            }
            catch (SystemException)
            {
                return 0;
            }
        }

        protected double ParseToDouble(string value)
        {
            try
            {
                return double.Parse(value);
            }
            catch (SystemException)
            {
                return 0;
            }
        }
    }
}
