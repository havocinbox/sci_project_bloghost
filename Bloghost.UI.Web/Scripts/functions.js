$(function () {
    $("#resetPasswordButton").click(function () {
        var passOld = $("#oldPassword").val();
        var passNew = $("#newPassword").val();
        var passNewRepeat = $("#confirmPassword").val();

        if (passOld == "" || passNew == "" || passNewRepeat == "") {
            $("#ResetPasswordError").show();
            $("#ResetPasswordErrorMessage").text("Fields can not be empty");
        } else if (passNew != passNewRepeat) {
            $("#ResetPasswordError").show();
            $("#ResetPasswordErrorMessage").text("New password does not equals to confirm password.");
        } else {
            $("#ResetPasswordError").hide();
            $.ajax({
                url: "/profile/resetpassword",
                type: "post",
                data: { "OldPassword": passOld, "NewPassword": passNew, "ConfirmPassword": passNewRepeat },
                success: function (text) {
                    if (text == "") {
                        $("#resetPaswordModel").find(".close").click();
                    } else {
                        $("#ResetPasswordError").show();
                        $("#ResetPasswordErrorMessage").text(text);
                    }
                }
            });
        }
    });

    $("#deleteProfileButton").click(function () {
        var pass = $("#password").val();
        var id = $("#userId").val();

        $("#Error").hide();
        $.ajax({
            url: "/profile/deleteprofile",
            type: "post",
            data: { "Password": pass, "UserId": id },
            success: function (text) {
                if (text == "") {
                    window.location.href = "/home";
                } else {
                    $("#deleteProfileModel").find(".close").click();
                    $("#Error").show();
                    $("#ErrorMessage").text(text);
                }
            }
        });
    });

    $("#updateProfileButton").click(function () {
        var name = $("#name").val();
        var aboutMe = $("#aboutMe").val();
        var month = $("#dateOfBirth_Month").val();
        var day = $("#dateOfBirth_Day").val();
        var year = $("#dateOfBirth_Year").val();
        var id = $("#userId").val();

        if (name == "") {
            $("#EditProfileError").show();
            $("#EditProfileErrorMessage").text("Name cannot be empty");
        } else {
            $.ajax({
                url: "/profile/UpdateProfile",
                type: "post",
                data: { "Name": name, "Id": id, "AboutUser": aboutMe, "DateOfBirthYear": year, "DateOfBirthMonth": month, "DateOfBirthDay": day },
                success: function (text) {
                    if (text == "") {
                        $("#Error").hide();
                        $("#EditProfileError").hide();
                        $("#updateProfileModel").find(".close").click();

                        $("#currentName").text(name);
                        $("#currentAboutMe").text(aboutMe);
                        $("#currentAge").text(2013 - parseInt(year, 10));

                        $("#Success").show();
                        $("#SuccessMessage").text("Profile update.");
                    } else {
                        $("#EditProfileError").show();
                        $("#EditProfileErrorMessage").text(text);
                    }
                }
            });
        }
    });

    $("#editBlogButton").click(function () {
        var title = $("#Title").val();
        var desc = $("#Description").val();
        var link = $("#blogLink").val();
        var id = $("#id").val();
        var userId = $("#userId").val();
        var action = $("#action").val();

        if (title == "") {
            $("#EditProfileError").show();
            $("#EditProfileErrorMessage").text("Title cannot be empty");
        } else {
            $.ajax({
                url: "/blog/EditBlog",
                type: "post",
                data: { "Title": title, "Id": id, "Description": desc, "BlogLink": link, "AuthorId": userId, "Action": action },
                success: function (text) {
                    if (text == "") {
                        $("#Error").hide();
                        $("#EditProfileError").hide();
                        $("#updateBlogModel").find(".close").click();

                        $("#currentTitle").text(title);
                        $("#currentDescription").text(desc);

                        $("#Success").show();
                        $("#SuccessMessage").text("Blog update.");
                    } else {
                        $("#EditProfileError").show();
                        $("#EditProfileErrorMessage").text(text);
                    }
                }
            });
        }
    });

    $("#deleteBlogButton").click(function () {
        var blogId = $("#blogId").val();

        $("#Error").hide();
        $.ajax({
            url: "/blog/deleteblog",
            type: "post",
            data: { "BlogId": blogId },
            success: function (text) {
                if (text == "") {
                    window.location.href = "/blogs";
                } else {
                    $("#deleteBlogModel").find(".close").click();
                    $("#Error").show();
                    $("#ErrorMessage").text(text);
                }
            }
        });
    });
    
    $("#deletePostButton").click(function () {
        var blogId = $("#blogId").val();

        $("#Error").hide();
        $.ajax({
            url: "/post/deletepost",
            type: "post",
            data: { "BlogId": blogId },
            success: function (text) {
                if (text == "") {
                    window.location.href = "/blogs";
                } else {
                    $("#deleteBlogModel").find(".close").click();
                    $("#Error").show();
                    $("#ErrorMessage").text(text);
                }
            }
        });
    });

    $("#createPostButton").click(function () {
        var title = $("#Title").val();
        var id = $("#id").val();
        var tags = $("#Tags").val();
        var blogId = $("#blogId").val();
        var content = $("#editor").html().toString();

        if (title == "" || content == "" || tags == "") {
            $("#JError").show();
            $("#JErrorMessage").text("All fields cannot be empty");
        } else {
            $.ajax({
                url: "/post/createpost",
                type: "post",
                data: { "Title": title, "Id": id, "BlogId": blogId, "Content": content, "Tags": tags },
                success: function (text) {
                    if (text == "") {
                        window.location.href = "/blogs";
                    } else {
                        $("#Error").show();
                        $("#ErrorMessage").text(text);
                    }
                }
            });
        }
    });
    
    $("#editPostButton").click(function () {
        var title = $("#Title").val();
        var id = $("#id").val();
        var tags = $("#Tags").val();
        var blogId = $("#blogId").val();
        var content = $("#editor").html().toString();

        if (title == "" || content == "" || tags == "") {
            $("#JError").show();
            $("#JErrorMessage").text("All fields cannot be empty");
        } else {
            $.ajax({
                url: "/post/editpost",
                type: "post",
                data: { "Title": title, "Id": id, "BlogId": blogId, "Content": content, "Tags": tags },
                success: function (text) {
                    if (text == "") {
                        window.location.href = "/blogs";
                    } else {
                        $("#Error").show();
                        $("#ErrorMessage").text(text);
                    }
                }
            });
        }
    });

    $("#addNewComment").click(function () {
        var message = $("#commentMessage").val();
        var postId = $("#postId").val();

        if (message != "") {
            $.ajax({
                url: "/post/addcomment",
                type: "post",
                data: { "Message": message, "PostId": postId },
                success: function (text) {
                    $("#commentList").append(text);
                }
            });
        }

        $("#commentMessage").val("");
    });










    //$("#blogLink").keyup(function () {
    //    var blogLink = $("#blogLink").val();
    //    var currentBlogLink = $("#currentBlogLink").val;
    //    if (blogLink != currentBlogLink) {
    //        $("#deleteBlogButton").attr("disabled", "disabled");
    //    } else {
    //        $("#deleteBlogButton").removeAttr("disabled");
    //    }
    //});
});