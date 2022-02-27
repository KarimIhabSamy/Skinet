namespace Core.Specifications
{
    public class ProductSpecParams
    {
        private const int MaxPaging = 50;
        public int PageIdex {get;set;} = 1;

        private int _pageSize=6;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPaging ?MaxPaging : value);
        }

        public int? BrandId {get;set;}
        public int? TypeId {get;set;}
        public string sort {get;set;}

    }
}