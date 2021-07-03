using System;

namespace TrueOrFalse.Models
{
    [Serializable]
    public class Statement : BaseModel
    {
        private string _text;
        private bool _isTrue;

        public Statement()
        { 
        }

        public Statement(string text, bool isTrue)
        {
            Text = text;
            IsTrue = isTrue;
        }

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        public bool IsTrue
        {
            get => _isTrue;
            set
            {
                _isTrue = value;
                OnPropertyChanged();
            }
        }
    }
}
