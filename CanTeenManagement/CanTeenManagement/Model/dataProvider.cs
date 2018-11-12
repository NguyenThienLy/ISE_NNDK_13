using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanTeenManagement.Model
{
    public class dataProvider
    {
        private static dataProvider instance;

        public static dataProvider Instance
        {
            get { if (dataProvider.instance == null) dataProvider.instance = new dataProvider(); return dataProvider.instance; }

            private set { dataProvider.instance = value; }
        }

        public QLCanTinEntities DB;

        private dataProvider()
        {
            DB = new QLCanTinEntities();
        }
    }
}
