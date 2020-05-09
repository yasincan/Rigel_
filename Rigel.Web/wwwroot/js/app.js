"use strict";

$(() => {
    if ($('#todoTable').length !== 0) {
        var i = 1;
        var table = $('#todoTable').DataTable({
            language: {
                "sDecimal": ",",
                "sEmptyTable": "Tabloda herhangi bir veri mevcut değil",
                "sInfo": "_TOTAL_ kayıttan _START_ - _END_ arasındaki kayıtlar gösteriliyor",
                "sInfoEmpty": "Kayıt yok",
                "sInfoFiltered": "(_MAX_ kayıt içerisinden bulunan)",
                "sInfoPostFix": "",
                "sInfoThousands": ".",
                "sLengthMenu": "Sayfada _MENU_ kayıt göster",
                "sLoadingRecords": "Yükleniyor...",
                "sProcessing": "İşleniyor...",
                "sSearch": "Ara:",
                "sZeroRecords": "Eşleşen kayıt bulunamadı",
                "lengthMenu": "İlk _MENU_ kaydı göster",
                "oPaginate": {
                    "sFirst": "İlk",
                    "sLast": "Son",
                    "sNext": "Sonraki",
                    "sPrevious": "Önceki"
                },
                "oAria": {
                    "sSortAscending": ": artan sütun sıralamasını aktifleştir",
                    "sSortDescending": ": azalan sütun sıralamasını aktifleştir"
                },
                buttons: {
                     lengthMenu: [
                         [10, 25, 50, -1],
                         ['10 ', '25 ', '50 ', 'Tümünü göster']
                     ]
                }
            },
            processing: true,
            serverSide: true,
            orderCellsTop: true,
            autoWidth: true,
            deferRender: true,
            lengthMenu: [[5, 10, 15, 20, -1], [5, 10, 15, 20, "All"]],
            dom: '<"row"<"col-sm-12 col-md-6"B><"col-sm-12 col-md-6 text-right"l>><"row"<"col-sm-12"tr>><"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>',
            buttons: [
                {
                    text: 'Export to Excel',
                    className: 'btn btn-sm btn-dark',
                    action: function (e, dt, node, config) {
                        window.location.href = "/Admin/Todo/GetExcel";
                    },
                    init: function (api, node, config) {
                        $(node).removeClass('dt-button');
                    }
                },
                {
                    text: 'Create',
                    className: 'btn btn-sm btn-success',
                    action: function (e, dt, node, config) {
                        $('#createModal').modal('show');
                    },
                    init: function (api, node, config) {
                        $(node).removeClass('dt-button');
                    }
                }
            ],
            ajax: {
                type: "POST",
                url: '/Admin/Todo/LoadTable/',
                contentType: "application/json; charset=utf-8",
                async: true,
                headers: {
                    "XSRF-TOKEN": document.querySelector('[name="__RequestVerificationToken"]').value
                },
                data: function (data) {
                    let additionalValues = [];
                    additionalValues[0] = "Additional Parameters 1";
                    additionalValues[1] = "Additional Parameters 2";
                    data.AdditionalValues = additionalValues;

                    return JSON.stringify(data);
                }
            },
            columns: [
                {
                    title: "Id",
                    data: "Id",
                    name: "co"
                },
                {
                    title: "Oluşturulma Tarihi",
                    data: "CreatedDate",
                    name: "lt"
                },
                {
                    title: "Güncellenme Tarihi",
                    data: "UpdatedDate",
                    name: "lte"
                },
                {
                    title: "Todo Adı",
                    data: "TodoName",
                    name: "co"
                },
                {
                    title: "Acıklama",
                    data: "Description",
                    name: "co"
                },
                {
                    title: "Aktif / Pasif",
                    data: "IsActive",
                    name: "eq"
                },
                {
                    title: "Düzenle / Sil",
                    orderable: false,
                    width: 100,
                    data: "",
                    render: function (data, type, row) {
                        return `<div>
                                    <button type="button" class="btn btn-sm btn-info mr-2 btnEdit" data-key="${row.Id}">Düzenle</button>
                                    <button type="button" class="btn btn-sm btn-danger btnDelete" data-key="${row.Id}">Sil</button>
                                </div>`;
                    }
                }
            ]
        });

        table.columns().every(function (index) {
            $('#todoTable thead tr:last th:eq(' + index + ') input')
                .on('keyup',
                    function (e) {
                        if (e.keyCode === 13) {
                            table.column($(this).parent().index() + ':visible').search(this.value).draw();
                        }
                    });
        });

        $(document)
            .off('click', '#btnCreate')
            .on('click', '#btnCreate', function () {
                fetch('/Admin/Todo/Create/',
                    {
                        method: 'POST',
                        cache: 'no-cache',
                        body: new URLSearchParams(new FormData(document.querySelector('#frmCreate')))
                    })
                    .then((response) => {
                        table.ajax.reload();
                        $('#createModal').modal('hide');
                        document.querySelector('#frmCreate').reset();
                    })
                    .catch((error) => {
                        console.log(error);
                    });
            });

        $(document)
            .off('click', '.btnEdit')
            .on('click', '.btnEdit', function () {
                const id = $(this).attr('data-key');

                fetch(`/Admin/Todo/Edit/${id}`,
                    {
                        method: 'GET',
                        cache: 'no-cache'
                    })
                    .then((response) => {
                        return response.text();
                    })
                    .then((result) => {
                        $('#editPartial').html(result);
                        $('#editModal').modal('show');
                    })
                    .catch((error) => {
                        console.log(error);
                    });
            });

        $(document)
            .off('click', '#btnUpdate')
            .on('click', '#btnUpdate', function () {
                fetch('/Admin/Todo/Edit/',
                    {
                        method: 'PUT',
                        cache: 'no-cache',
                        body: new URLSearchParams(new FormData(document.querySelector('#frmEdit')))
                    })
                    .then((response) => {
                        table.ajax.reload();
                        $('#editModal').modal('hide');
                        $('#editPartial').html('');
                    })
                    .catch((error) => {
                        console.log(error);
                    });
            });

        $(document)
            .off('click', '.btnDelete')
            .on('click', '.btnDelete', function () {
                const id = $(this).attr('data-key');

                if (confirm('Are you sure?')) {
                    fetch(`/Admin/Todo/Delete/${id}`,
                        {
                            method: 'DELETE',
                            cache: 'no-cache'
                        })
                        .then((response) => {
                            table.ajax.reload();
                        })
                        .catch((error) => {
                            console.log(error);
                        });
                }
            });

        $('#btnExternalSearch').click(function () {
            table.column('0:visible').search($('#txtExternalSearch').val()).draw();
        });
    }
});
