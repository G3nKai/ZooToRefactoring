using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo
{
    interface ICageClosed 
    {
        int countAnimalsClosed();
        void moveToOpened(Animal animal);
        void getInfo();
    }
}
