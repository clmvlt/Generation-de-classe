using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Attribu
    {
        #region Attribus
        private string _name;
        private string _type;
        private bool _isGetter;
        private bool _isSetter;

        #endregion

        #region Constructeur
        public Attribu(string name, string type, bool isGetter, bool isSetter)
        {
            Name = name;
            Type = type;
            IsGetter = isGetter;
            IsSetter = isSetter;
        }

        #endregion

        #region Properties
        public string Name { get => _name; set => _name = value; }
        public string Type { get => _type; set => _type = value; }
        public bool IsGetter { get => _isGetter; set => _isGetter = value; }
        public bool IsSetter { get => _isSetter; set => _isSetter = value; }
        #endregion

        #region Methodes
        public override string ToString()
        {
            return $"===========================\nNom : {Name}\nType : {Type}\nGetteur : {IsGetter}\nSetteur : {IsSetter}\n===========================";
        }
        #endregion
    }
}
