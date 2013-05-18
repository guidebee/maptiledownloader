using System.Threading;
using MapDigit.GIS.Drawing;

namespace MapDigit.Drawing
{
    class NETDisplay : IDisplay
    {


        private static readonly NETDisplay INSTANCE = new NETDisplay();


        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 17NOV2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Private constructor to prevent instanciation
         */
        private NETDisplay()
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 17NOV2008  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Return the Display instance
         *
         * @return the Display instance
         */
        public static IDisplay getInstance()
        {
            return INSTANCE;
        }

        public int GetDisplayHeight()
        {
            return 768;
        }

        public int GetDisplayWidth()
        {
            return 768;
        }

        public bool IsEdt()
        {
            return false;
        }

        public void CallSerially(ThreadStart r)
        {

        }

    }

}
