using CanTeenManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CanTeenManagement.Model
{
    class PAYFOOD : BaseViewModel
    {
        private string _ID;
        public string ID
        {
            get => _ID;
            set
            {
                _ID = value;
                OnPropertyChanged();
            }
        }

        private string _FOODNAME;
        public string FOODNAME
        {
            get => _FOODNAME;
            set
            {
                _FOODNAME = value;
                OnPropertyChanged();
            }
        }

        private int _FOODTYPE;
        public int FOODTYPE
        {
            get => _FOODTYPE;
            set
            {
                _FOODTYPE = value;
                OnPropertyChanged();
            }
        }

        private int _PRICE;
        public int PRICE
        {
            get => _PRICE;
            set
            {
                _PRICE = value;
                OnPropertyChanged();
            }
        }

        private int _PRICESALE;
        public int PRICESALE
        {
            get => _PRICESALE;
            set
            {
                _PRICESALE = value;
                OnPropertyChanged();
            }
        }

        private int _SALE;
        public int SALE
        {
            get => _SALE;
            set
            {
                _SALE = value;
                OnPropertyChanged();
            }
        }

        public string _IMAGELINK;
        public string IMAGELINK
        {
            get => _IMAGELINK;
            set
            {
                _IMAGELINK = value;
                OnPropertyChanged();
            }
        }

        private int _QUANTITY;
        public int QUANTITY
        {
            get => _QUANTITY;
            set
            {
                int i = 0;
                if (value != 0)
                {
                    if (!int.TryParse(value.ToString(), out i))
                        value = QUANTITY;
                    else if (value < 0)
                        value = QUANTITY;
                    else if (value > 10)
                        value = QUANTITY;
                }

                _QUANTITY = value;
                OnPropertyChanged();
            }
        }

        private bool _ISCHECKED;
        public bool ISCHECKED
        {
            get => _ISCHECKED;
            set
            {
                _ISCHECKED = value;
                OnPropertyChanged();
            }
        }

        private bool _ISENABLEQUANTITY;
        public bool ISENABLEQUANTITY
        {
            get => _ISENABLEQUANTITY;
            set
            {
                _ISENABLEQUANTITY = value;
                OnPropertyChanged();
            }
        }

        private ImageSource _IMAGESOURCE;
        public ImageSource IMAGESOURCE
        {
            get => _IMAGESOURCE;
            set
            {
                _IMAGESOURCE = value;
                OnPropertyChanged();
            }
        }

        private string _VISIBILITY;
        public string VISIBILITY
        {
            get => _VISIBILITY;
            set
            {
                _VISIBILITY = value;
                OnPropertyChanged();
            }
        }

        public PAYFOOD() { }

        public PAYFOOD(ORDERFOOD orderFood)
        {
            this.ID = orderFood.ID;
            this.FOODNAME = orderFood.FOODNAME;
            this.FOODTYPE = orderFood.FOODTYPE;
            this.PRICE = orderFood.PRICE;
            this.PRICESALE = orderFood.PRICESALE;
            this.SALE = orderFood.SALE;
            this.IMAGELINK = orderFood.IMAGELINK;
            this.IMAGESOURCE = orderFood.IMAGESOURCE;
            this.VISIBILITY = orderFood.VISIBILITY;

            // Default quantity  = 1.
            this.QUANTITY = 1;
            this.ISCHECKED = true;
            this.ISENABLEQUANTITY = true;
        }

        public PAYFOOD(PAYFOOD payFood)
        {
            this.ID = payFood.ID;
            this.FOODNAME = payFood.FOODNAME;
            this.FOODTYPE = payFood.FOODTYPE;
            this.PRICE = payFood.PRICE;
            this.PRICESALE = payFood.PRICESALE;
            this.SALE = payFood.SALE;
            this.IMAGELINK = payFood.IMAGELINK;
            this.QUANTITY = payFood.QUANTITY;
            this.ISCHECKED = payFood.ISCHECKED;
        }
    }
}
