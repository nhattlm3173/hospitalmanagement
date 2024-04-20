// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Lắng nghe sự kiện click trên nút toggleButton
document.getElementById("toggleButton").addEventListener("click", function() {
    // Lấy phần tử div collapse
    var collapseDiv = document.getElementById("collapse-div");
    // Kiểm tra trạng thái hiện tại của collapseDiv
    if (collapseDiv.classList.contains("visible")) {
        // Nếu đã hiển thị, ẩn đi bằng cách xóa lớp "visible"
        
        collapseDiv.classList.remove("visible");
    } else {
        // Nếu đang ẩn, hiển thị bằng cách thêm lớp "visible"
        collapseDiv.classList.add("visible");
    }

});
// Kiểm tra kích thước cửa sổ và đóng collapse nếu cần
window.addEventListener("resize", function () {
    var windowWidth = window.innerWidth;
    console.log(windowWidth)
    if (windowWidth > 1024) {
        
        var collapseDiv = document.getElementById("collapse-div");
        if (collapseDiv.classList.contains("visible")) {
            collapseDiv.classList.remove("visible");
        }
    }
    if (windowWidth == 1229) {
        location.reload();
    }
});

//async function initMap() {
//    const contentString = `
//    <div>
//      <h1>Uluru</h1>
//      <div>
//        <p>
//          <b>Uluru</b>, also referred to as <b>Ayers Rock</b>, is a large
//          sandstone rock formation in the southern part of the
//          Northern Territory, central Australia. It lies 335&#160;km (208&#160;mi)
//          south west of the nearest large town, Alice Springs; 450&#160;km
//          (280&#160;mi) by road. Kata Tjuta and Uluru are the two major
//          features of the Uluru - Kata Tjuta National Park. Uluru is
//          sacred to the Pitjantjatjara and Yankunytjatjara, the
//          Aboriginal people of the area. It has many springs, waterholes,
//          rock caves and ancient paintings. Uluru is listed as a World
//          Heritage Site.
//        </p>
//        <p>
//          Attribution: Uluru,
//          <a href="https://en.wikipedia.org/w/index.php?title=Uluru&oldid=297882194">
//            https://en.wikipedia.org/w/index.php?title=Uluru
//          </a>
//          (last visited June 22, 2009).
//        </p>
//      </div>
//    </div>`;
//    const infoWindow = new google.maps.InfoWindow({
//        content: contentString,
//        ariaLabel: "Uluru",
//    });

//    const marker = document.querySelector('gmp-advanced-marker');
//    marker.addEventListener('gmp-click', () => {
//        infoWindow.open({ anchor: marker });
//    });
//}

//window.initMap = initMap;
$(function () {
    $('#specialistSelect').change(function () {
        var specialistId = $(this).val();
        $.ajax({
            url: '/Appointment/GetDoctorsBySpecialist', // Đường dẫn đến action GetDoctorsBySpecialist
            type: 'GET',
            data: { specialistId: specialistId },
            success: function (data) {
                $('#doctorSelect').empty();
                $.each(data, function (index, item) {
                    $('#doctorSelect').append($('<option>').text(item.text).val(item.value));
                });
            }
        });
    });
});
