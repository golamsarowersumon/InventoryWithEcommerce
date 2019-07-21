$(function () {

    $("#StoreName").autocomplete({

        source: function (request, response) {
            alert("Go");
            $.ajax({
                url: "/Admin/Search",
                type: "POST",
                dataType: "json",
                data: { StoreName: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.Name, value: item.SID };
                    }))

                }
            })
        },
        messages: {
            noResults: "",
            results: function (count) {
                return count + (count > 1 ? ' results' : ' result ') + ' found';
            }
        }
    });


});