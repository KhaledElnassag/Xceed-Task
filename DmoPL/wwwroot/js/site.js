$("#AccountId").on("change", function () {
            debugger;
    var accountValue = $(this).val();
    console.log(accountValue);
    fetch("https://localhost:44386/Employee/GetBusinessLines/" + accountValue)
        .then(res => res.json())
        .then(jsonD => {
            console.log(jsonD);
            const BusinessList = $("#BusinessId");

            BusinessList.empty(); 

            jsonD.forEach(businessLine => {
                const option = $("<option>")
                    .val(businessLine.name)
                    .text(businessLine.name);
                BusinessList.append(option);
            });
        })
        .catch(error => console.log(error));
});


$("#DateId").on("input", function () {
    //debugger;
    var currentDate = new Date().getFullYear();
    var dateVal = new Date($(this).val()).getFullYear();
    var age = currentDate - dateVal;
    $("#AgeId").val(age<0?0:age);
})


