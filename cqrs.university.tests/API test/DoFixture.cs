using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cqrs.university.tests
{
    public class DoFixture : fitlibrary.DoFixture
    {
        private String contents;
        public void FillTimesWith(int howmany, String what)
        {
            contents = "";
            for (int i = 0; i < howmany; i++)
            {
                contents = contents + what;
            }
        }

        public bool CharAtIs(int index, char c)
        {
            return contents[index] == c;
        }
    }
}
