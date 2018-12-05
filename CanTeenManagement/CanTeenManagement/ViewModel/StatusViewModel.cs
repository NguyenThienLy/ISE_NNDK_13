using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using CanTeenManagement.CO;

namespace CanTeenManagement.ViewModel
{
    class StatusViewModel : BaseViewModel
    {
        private string _g_str_icon;
        public string g_str_icon
        {
            get => _g_str_icon;

            set
            {
                _g_str_icon = value;
                OnPropertyChanged();
            }
        }

        private string _g_str_borderBrushWindow;
        public string g_str_borderBrushWindow
        {
            get => _g_str_borderBrushWindow;

            set
            {
                _g_str_borderBrushWindow = value;
                OnPropertyChanged();
            }
        }

        private string _g_str_borderBrushButton;
        public string g_str_borderBrushButton
        {
            get => _g_str_borderBrushButton;

            set
            {
                _g_str_borderBrushButton = value;
                OnPropertyChanged();
            }
        }

        private string _g_str_foregroundIcon;
        public string g_str_foregroundIcon
        {
            get => _g_str_foregroundIcon;

            set
            {
                _g_str_foregroundIcon = value;
                OnPropertyChanged();
            }
        }

        private string _g_str_text;
        public string g_str_text
        {
            get => _g_str_text;

            set
            {
                _g_str_text = value;
                OnPropertyChanged();
            }
        }

        private bool _g_b_isSuccessful;
        public bool g_b_isSuccessful
        {
            get => _g_b_isSuccessful;

            set
            {
                _g_b_isSuccessful = value;
                OnPropertyChanged();
            }
        }

        private Window _g_wd_p { get; set; }

        #region command.
        public ICommand g_iCm_LoadedCommand { get; set; }
        #endregion


        public StatusViewModel()
        {
            g_iCm_LoadedCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                this._g_wd_p = p;
                this.load(p);
            });   
        }

        private void load(Window p)
        {
            this.startCloseTimer();

            this.setLocation(p);

            if (this.g_b_isSuccessful == true)
            {
                this.loadSuccessful();
            }
            else
            {
                this.loadFail();
            }
        }

        private void loadSuccessful()
        {
            this.g_str_icon = "DoneOutline";
            this.g_str_borderBrushWindow = "Green";
            this.g_str_borderBrushButton = "Green";
            this.g_str_foregroundIcon = "Green";
        }

        private void loadFail()
        {
            this.g_str_icon = "ErrorOutline";
            this.g_str_borderBrushWindow = "Red";
            this.g_str_borderBrushButton = "Red";
            this.g_str_foregroundIcon = "Red";
        }

        private void setLocation(Window p)
        {
            //staticVarClass.quantity_statusView++;

            p.Left = staticVarClass.screen_Left;
            p.Top = staticVarClass.screen_Top;
        }

        private void startCloseTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1d);
            timer.Tick += timerTick;

            timer.Start();
        }

        private void timerTick(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
            timer.Tick -= timerTick;

            this._g_wd_p.Close();

            //staticVarClass.quantity_statusView--;
        }
    }
}
