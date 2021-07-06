using System;

namespace TrueOrFalse.Models
{
    [Serializable]
    public class Statement : BaseModel
    {
        private static readonly Statement _empty = new(default, default);
        private string _text;
        private bool _isTrue;

        public Statement(string text, bool isTrue)
        {
            Text = text;
            IsTrue = isTrue;
        }

        public static Statement Empty => _empty;

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

        public bool HasEqualValues(Statement other)
        {
            return Text == other.Text && IsTrue == other.IsTrue;
        }
    }
}
