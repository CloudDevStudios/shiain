using System.Collections.Generic;
using UnityEngine;

namespace Adrenak.Shiain {
	public class Pagination {
		public int DataLen { get; private set; }
		public int PageLen { get; private set; }
		public int Index { get; private set; }

		public Pagination(int dataLen, int pageLen) {
			DataLen = dataLen;
			PageLen = pageLen;
			Index = 0;
		}

		public object[] Get(object[] data) {
			var result = new List<object>();
			int upto = Mathf.Min((Index + 1) * PageLen, DataLen);

			for (int i = Index * PageLen; i < upto; i++)
				result.Add(data[i]);

			return result.ToArray();
		}

		public void Reset() {
			Index = 0;
		}

		public bool Next() {
			if (Index == MaxIndex)
				return false;
			Index++;
			return true;
		}

		public bool Previous() {
			if (Index == 0) return false;
			Index--;
			return true;
		}

		public int MaxIndex {
			get { return (DataLen - 1) / PageLen; }
		}
	}
}
