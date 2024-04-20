window.addEventListener('DOMContentLoaded', event => {
    // Simple-DataTables
    // https://github.com/fiduswriter/Simple-DataTables/wiki
    let options = {
        searchable: true,
        perPage: 5,
        ellipsisText: "...",
        rowNavigation: true,
        fixedColumns: false,
        labels: {
            placeholder: "Tìm Kiếm...",
            searchTitle: "Search within table",
            pageTitle: "Trang {page}",
            perPage: "Mục Mỗi Trang",
            noRows: "Không Có Dữ Liệu",
            info: "{start} trên {end} trang",
            noResults: "Không tìm thấy kết quả",
        }
    };
    const datatablesSimple = document.getElementById('datatablesSimple');
    if (datatablesSimple) {
        new simpleDatatables.DataTable(datatablesSimple, options);
    }
});
