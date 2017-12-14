﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace RBlogOnNetCore.Infrastructures
{
    public abstract class BasePageableModel : IPageableModel
    {
        #region Properties
        public int PageIndex
        {
            get
            {
                if (PageNumber > 0)
                    return PageNumber - 1;
                else
                    return 0;
            }
        }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages { get; set; }

        public int FirstItem { get; set; }

        public bool HasPreviousPage { get; set; }

        public bool HasNextPage { get; set; }
        public int LastItem { get; set; }
        #endregion
        #region Methods
        public IEnumerator GetEnumerator()
        {
            return new List<string>().GetEnumerator();
        }
        public virtual void LoadPagedList<T>(IPagedList<T> pagedList)
        {
            FirstItem = (pagedList.PageIndex * pagedList.PageSize) + 1;
            HasNextPage = pagedList.HasNextPage;
            HasPreviousPage = pagedList.HasPreviousPage;
            LastItem = Math.Min(pagedList.TotalCount, ((pagedList.PageIndex * pagedList.PageSize) + pagedList.PageSize));
            PageNumber = pagedList.PageIndex + 1;
            PageSize = pagedList.PageSize;
            TotalItems = pagedList.TotalCount;
            TotalPages = pagedList.TotalPages;
        }
        #endregion
    }
}
