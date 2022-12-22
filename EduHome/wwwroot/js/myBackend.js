let count = 6;
let procount = $("#loadBtn").next().val();
console.log(procount)
$(document).on("click", "#loadBtn", function () {
    $.ajax({
        url: "/Courses/LoadMore/",
        type: "get",
        data: {
            "skip":count
        },
        success: function (res) {
            $("#myCourses").append(res)
            count += 6;
            if (procount <= count) {
                $("#loadBtn").remove()
            }
        }
    });
});