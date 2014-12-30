using System.Speech.Synthesis;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace SaySorry.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private RelayCommand _startToSay;
        private string _text;

        public MainViewModel()
        {
            if (IsInDesignMode)
                _text = "1st sorry";
        }

        public RelayCommand StartToSay
        {
            get
            {
                return _startToSay ?? (_startToSay = new RelayCommand(() =>
                {
                    Task.Run(() =>
                    {
                        var speaker = new SpeechSynthesizer();
                        for (var i = 1; i <= 100000; ++i)
                        {
                            var th = "th";
                            if (i%100/10 != 1)
                            {
                                switch (i%10)
                                {
                                    case 1:
                                        th = "st";
                                        break;

                                    case 2:
                                        th = "nd";
                                        break;

                                    case 3:
                                        th = "rd";
                                        break;
                                }
                            }
                            speaker.Speak(Text = string.Format("{0}{1} sorry", i, th));
                        }
                    });
                }));
            }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                if (_text == value)
                    return;

                _text = value;
                RaisePropertyChanged(() => Text);
            }
        }
    }
}