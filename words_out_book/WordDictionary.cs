using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace words_out_book
{
    public class WordDictionary<TKey,TValue> :Dictionary<TKey,TValue>
    {
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(TKey x in Keys)
            {
                sb.Append("key: "+x + " value: "+this[x]);
            }
            return sb.ToString();
        }
    }
}
