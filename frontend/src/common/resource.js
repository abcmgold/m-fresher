const MISAResource = {
    'vn-VI': {
        nameApp: "MISA QLTS",
        yes: "Đồng ý",
        no: "Không",
        cancel: "Hủy",
        save: "Lưu",
        addProperty: "Thêm tài sản",
        updateProperty: "Sửa tài sản",
        cancelEditting: "Bạn có muốn hủy bỏ chỉnh sửa tài sản này?",
        declareProperty: "Bạn có muốn hủy bỏ khai báo tài sản này?",
        total: "Tổng số",
        record: "bản ghi",
        addPropertySuccess: "Thêm tài sản thành công",
        updatePropertySuccess: "Chỉnh sửa tài sản thành công",
        duplicatePropertyCodeError: "Mã tài sản đã tồn tại",
        dayInDatePicker: [
            'T2',
            'T3',
            'T4',
            'T5',
            'T6',
            'T7',
            'CN'
        ],
        hide: 'Hiển thị',
        chooseAtLeastOne: 'Vui lòng chọn ít nhất 1 bản ghi!',
        confirm: 'Bạn có muốn xóa tài sản',
        deleteMultipleMessage: ' tài sản đã được chọn. Bạn có muốn xóa các tài sản này khỏi danh sách?',
        close: 'Đóng',
        wearRateError: 'Tỷ lệ hao mòn phải bằng 1/Số năm sử dụng',
        wearRateValueError: 'Hao mòn năm phải nhỏ hơn hoặc bằng nguyên giá',
        rowPerPage: 'Số dòng/trang:',
        searchProperty: 'Tìm kiếm tài sản',
        propertyType: 'Loại tài sản',
        departmentUse: 'Bộ phận sử dụng',
        reload: "Cập nhật dữ liệu",
        addProperties: "+ Thêm tài sản",
        addDocument: "Thêm chứng từ",
        dateError: "Giá trị ngày bắt đầu sử dụng phải lớn hơn hoặc bằng ngày mua!",
        order: "Số thứ tự",
        noResult: "Không có kết quả",
        saveOrNot: 'Thông tin đã thay đổi, bạn có muốn lưu không?',
        dontSave: 'Không lưu',
        noData: 'Không có dữ liệu',
        deleteSuccess: 'Xóa thành công',
        addTransferAssetSuccess: 'Thêm chứng từ thành công',
        updateTransferAssetSuccess: 'Chỉnh sửa chứng từ thành công',
        moveUp: "Di chuyển lên",
        moveDown: "Di chuyển xuống",
        addReceiverField: "Thêm mới",
        property: {
            propertyCode: 'Mã tài sản',
            propertyCodeError: 'Mã tài sản không được phép để trống',
            propertyCodeType: 'Nhập mã tài sản',

            propertyName: 'Tên tài sản',
            propertyNameError: 'Tên tài sản không được phép để trống',
            propertyNameType: 'Nhập tên tài sản',

            departmentCode: 'Mã bộ phận sử dụng',
            departmentCodeError: 'Mã bộ phận sử dụng không được phép để trống',
            departmentCodeType: 'Nhập mã bộ phận sử dụng',

            departmentName: 'Tên bộ phận sử dụng',

            propertyTypeCode: 'Mã loại tài sản',
            propertyTypeCodeError: 'Mã loại tài sản không được phép để trống',
            propertyTypeCodeType: 'Nhập mã loại tài sản',

            propertyTypeName: 'Tên loại tài sản',
            propertyTypeNameError: 'Tên loại tài sản không được phép để trống',

            quantity: 'Số lượng',
            quantityError: 'Số lượng không được phép để trống',

            originalPrice: 'Nguyên giá',
            originalPriceError: 'Nguyên giá không được phép để trống',

            numberYearUse: 'Số năm sử dụng',
            numberYearUseError: 'Số năm sử dụng không được phép để trống',

            wearRate: 'Tỷ lệ hao mòn (%)',
            wearRateError: 'Tỷ lệ hao mòn không được phép để trống',

            wearRateValue: 'Giá trị hao mòn năm',
            wearRateValueError: 'Giá trị hao mòn năm không được phép để trống',

            followYear: 'Năm theo dõi',
            followYearError: 'Năm theo dõi không được phép để trống',

            purchaseDate: 'Ngày mua',
            purchaseDateError: 'Ngày mua không được phép để trống',

            useDate: 'Ngày bắt đầu sử dụng',
            useDateError: 'Ngày bắt đầu sử dụng không được phép để trống',
        },

        listHeader: [
            {
                name: 'Mã tài sản',
                width: '150px',
                align: 'text-align-left',
                field: 'PropertyCode'
            },
            {
                name: 'Tên tài sản',
                width: '300px',
                align: 'text-align-left',
                field: 'PropertyName'

            }, {
                name: 'Loại tài sản',
                width: '250px',
                align: 'text-align-left',
                field: 'PropertyTypeName'
            }, {
                name: 'Bộ phận sử dụng',
                width: '250px',
                align: 'text-align-left',
                field: 'DepartmentName'
            }, {
                name: 'Số lượng',
                width: '100px',
                align: 'text-align-right',
                field: 'Quantity',
                money: true,
                summaryField: 'TotalQuantity'
            }, {
                name: 'Nguyên giá',
                width: '150px',
                align: 'text-align-right',
                field: 'OriginalPrice',
                money: true,
                summaryField: 'TotalOriginalPrice'
            }, {
                name: 'HM/ KH lũy kế',
                width: '150px',
                fullName: 'Hao mòn/ Khấu hao lũy kế',
                align: 'text-align-right',
                field: 'WearRateValue',
                money: true,
                summaryField: 'TotalWearRateValue'
            }, {
                name: 'Giá trị còn lại',
                width: '150px',
                align: 'text-align-right',
                field: 'ResidualPrice',
                money: true,
                summaryField: 'TotalResidualPrice',
            }
        ],
        listHeaderTranfer: [
            {
                name: 'Mã chứng từ',
                style: 'width: 150px',
                align: 'text-align-left',
                field: 'TransferAssetCode',

            },
            {
                name: 'Ngày chứng từ',
                style: 'width: 200px',
                align: 'text-align-center',
                field: 'TransactionDate',
                date: true
            }, {
                name: 'Ngày điều chuyển',
                style: 'width: 200px',
                align: 'text-align-center',
                field: 'TransferDate',
                date: true
            }, {
                name: 'Nguyên giá',
                style: 'width: 150px',
                align: 'text-align-right',
                field: 'OriginalPrice',
                money: true,
                summaryField: 'totalPrice'
            }, {
                name: 'Giá trị còn lại',
                style: 'width: 150px',
                align: 'text-align-right',
                field: 'ResidualPrice',
                money: true,
                summaryField: 'totalResidualPrice'
            }, {
                name: 'Ghi chú',
                style: 'flex: 1',
                align: 'text-align-left',
                field: 'Note',
            }
        ],
        listChoosenTranfer: [
            {
                name: 'checkbox',
                style: 'width: 50px',
                align: 'text-align-center'
            },
            {
                name: 'STT',
                style: 'width: 50px',
                fullName: 'Số thứ tự',
                align: 'text-align-center'
            },
            {
                name: 'Mã tài sản',
                style: 'width: 150px',
                align: 'text-align-left'
            },
            {
                name: 'Tên tài sản',
                style: 'width: 200px',
                align: 'text-align-left'
            }, {
                name: 'Bộ phận sử dụng',
                style: 'width: 200px',
                align: 'text-align-left'
            }, {
                name: 'Nguyên giá',
                style: 'width: 150px',
                align: 'text-align-right'
            }, {
                name: 'Giá trị còn lại',
                style: 'width: 150px',
                align: 'text-align-right'
            }, {
                name: 'Năm theo dõi',
                style: 'width: 150px',
                align: 'text-align-right'
            }
        ],
        listHeaderDetailTranfer: [
            {
                name: 'STT',
                style: 'width: 50px',
                fullName: 'Số thứ tự',
                align: 'text-align-center'
            },
            {
                name: 'Mã tài sản',
                style: 'width: 120px',
                align: 'text-align-left'
            },
            {
                name: 'Tên tài sản',
                style: 'width: 160px',
                align: 'text-align-left'
            },
            {
                name: 'Nguyên giá',
                style: 'width: 160px',
                align: 'text-align-right'
            }, {
                name: 'Giá trị còn lại',
                style: 'width: 160px',
                align: 'text-align-right'
            }, {
                name: 'Bộ phận sử dụng',
                style: 'width: 200px',
                align: 'text-align-left'
            }, {
                name: 'Bộ phận điều chuyển đến',
                style: 'width: 200px',
                align: 'text-align-left'
            }, {
                name: 'Lý do',
                style: 'flex: 1',
                align: 'text-align-left'
            },
        ],
        listHeaderUpdateTranfer: [
            {
                name: 'checkbox',
                style: 'width: 50px',
                align: 'text-align-center'
            },
            {
                name: 'STT',
                style: 'width: 50px',
                fullName: 'Số thứ tự',
                align: 'text-align-center'
            },
            {
                name: 'Mã tài sản',
                style: 'width: 150px',
                align: 'text-align-left'
            },
            {
                name: 'Tên tài sản',
                style: 'width: 150px',
                align: 'text-align-left'
            }, {
                name: 'Nguyên giá',
                style: 'width: 150px',
                align: 'text-align-right'
            }, {
                name: 'Giá trị còn lại',
                style: 'width: 150px',
                align: 'text-align-right'
            }, {
                name: 'Bộ phận sử dụng',
                style: 'width: 180px',
                align: 'text-align-left'
            }, {
                name: 'Bộ phận điều chuyển đến',
                style: 'width: 220px',
                align: 'text-align-left'
            }, {
                name: 'Lý do',
                style: 'flex: 1',
                align: 'text-align-left'
            }, {
                name: 'Chức năng',
                style: 'width: 100px',
                align: 'text-align-center'
            },
        ],
        overview: "Tổng quan",
        propertyText: "Tài sản",
        propertyHt: "Tài sản HT-ĐB",
        propertyHtFull: "Tài sản hợp thành",

        tool: "Công cụ dụng cụ",
        menu: "Danh mục",
        search: "Tra cứu",
        find: "Tìm kiếm",
        report: "Báo cáo",
        exportExcel: "Xuất file excel",
        delete: "Xóa",
        duplicate: "Nhân bản",
        notification: "Thông báo",
        setup: "Cài đặt",
        help: "Hỏi đáp",
        chat: "Nhắn tin",
        profile: "Thông tin cá nhân",
        departmentOfFinance: "Sở tài chính",
        year: "Năm",
        propertyList: "Danh sách tài sản",
        edit: "Chỉnh sửa",
        serverNotResponse: "Máy chủ không có phản hồi!",

        // resources trong file PropertyTransfer.vue
        propertyTransfer: {
            transfer: "Điều chuyển",
            selected: "Đã chọn : ",
            unselected: "Bỏ chọn",
            delete: "Xóa",
            detail: "Thông tin chi tiết",
            generalInfo: "Thông tin chung",
            wantToDeleteTransferAsset: "Bạn có muốn xóa chứng từ",
            longWantToDeleteTransferAsset: 'chứng từ đã được chọn, bạn có muốn xóa các chứng từ này không'
        },
        // resource trong propertyTransferForm.vue
        propertyTransferForm: {
            generalInfo: "Thông tin chung",
            transferAssetCode: "Mã chứng từ",
            typeTransferAssetCode: "Nhập mã chứng từ",
            transferDate: "Ngày điều chuyển",
            typeTransferDate: "Nhập ngày điều chuyển",
            transactionDate: "Ngày chứng từ",
            typeTransactionDate: "Nhập ngày chứng từ",
            note: "Ghi chú",
            chooseReceiver: "Chọn ban giao nhận",
            addMoreReceiver: "Thêm ban giao nhận từ lần nhập trước",
            order: "STT",
            fullName: "Họ và tên",
            represent: "Đại diện",
            position: "Chức vụ",
            infoTransferAsset: "Thông tin tài sản điều chuyển",
            selected: "Đã chọn: ",
            unselected: "Bỏ chọn",
            chooseProperty: "Chọn tài sản",
            updateTransferAsset: "Sửa chứng từ điều chuyển",
            insertTransferAsset: "Thêm chứng từ điều chuyển",
            cancelAddTransferAsset: "Bạn có muốn hủy bỏ khai báo chứng từ này không?",
            cancelUpdateTransferAsset: "Bạn có muốn hủy bỏ chỉnh sửa chứng từ này không?",
            noChangeToSave: "Vui lòng thay đổi thông tin chứng từ để thực hiện cập nhật!",
            transferAssetCodeError: "Mã chứng từ không được phép trống!",
            transferAssetTransferDateError: "Ngày điều chuyển không được phép trống!",
            transferAssetTransactionDateError: "Ngày chứng từ không được phép trống!",
            noEmptyReceiver: 'Thông tin ban giao nhận không được để trống!<br>',
            noEmptyTransferAsset: 'Danh sách tài sản điều chuyển không được để trống!<br>',
            errorTransferDate: 'Ngày chứng từ phải nhỏ hơn ngày điều chuyển!<br>',
            differentDepartment: 'Bộ phận sử dụng mới phải khác bộ phận cũ!'
        },
        // Resource trong ChoosenForm.vue
        choosenForm: {
            choosePropertyTransfer: 'Chọn tài sản điều chuyển',
            newDepartment: "Bộ phận sử dụng mới",
            chooseNewDepartment: "Chọn bộ phận sử dụng mới",
            note: "Ghi chú",
            selected: "Đã chọn : ",
            unselected: "Bỏ chọn",
            pleaseChooseTransferAsset: "Vui lòng chọn tài sản điều chuyển",
            pleaseChooseNewDepartment: "Vui lòng chọn bộ phận điều chuyển mới"
        }
    }
}

export {
    MISAResource
}
