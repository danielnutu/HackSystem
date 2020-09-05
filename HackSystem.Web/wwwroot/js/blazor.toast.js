﻿window.toasts = {
    popToast: function (id, autohide = true, delay = 3000) {
        let toast = $(`#${id}`);
        toast.toast({
            animation: true,
            autohide: autohide,
            delay: delay
        });
        toast.on('hide.bs.toast', function (e) {
            $(e.target).slideUp(150);
        });
        toast.on('hidden.bs.toast', function () {

        });
        toast.toast('show');
    }
};