<template>
    <div class="property-tranfer--container" ref="container">
        <div class="tranfer__navbar">
            <div class="tranfer__navbar--left">
                <div class="no-selected-document" v-if="!this.selectedRow.length">
                    Điều chuyển
                    <button class="btn btn btn--noborder content__btn--reload" @click="handleReload">
                        <div
                            class="icon--reload"
                            :content="this.$_MISAResource['vn-VI'].reload"
                            v-tippy="{ placement: 'top' }"
                        ></div>
                    </button>
                </div>
                <div class="selected-document" v-if="this.selectedRow.length">
                    <div class="number-select">
                        Đã chọn: <strong>{{ this.selectedRow.length }}</strong>
                    </div>
                    <m-button individualClass="btn--noborder" label="Bỏ chọn" @click="removeAllSelected"></m-button>
                    <m-button individualClass="btn--sub" label="Xóa" @click="deleteTransferAssets"></m-button>
                </div>
            </div>
            <div class="tranfer__navbar--right">
                <m-button
                    :label="this.$_MISAResource['vn-VI'].addDocument"
                    :individualClass="'btn--primary'"
                    icon="icon--add"
                    @click="() => this.showTransferForm(this.$_MISAEnum.formAdd)"
                ></m-button>
                <div
                    class="icon--question"
                    :content="this.$_MISAResource['vn-VI'].help"
                    v-tippy="{ placement: 'bottom' }"
                ></div>
                <div
                    class="icon--comment"
                    :content="this.$_MISAResource['vn-VI'].chat"
                    v-tippy="{ placement: 'bottom' }"
                ></div>
            </div>
        </div>
        <div class="tranfer__content" :class="{ 'hidden-detail': !this.isShowDetailDocument }">
            <div class="table__content">
                <div class="table__content--header">
                    <div
                        class="table__content--header-item cell--item"
                        v-for="(header, index) in this.listHeaderTranfer"
                        :class="{ 'cell--item--checkbox': header.name == 'checkbox' }"
                        :key="index"
                        :style="header.style"
                    >
                        <template v-if="header.name === 'checkbox'">
                            <m-checkbox
                                ref="checkbox-all"
                                type="primary"
                                :class="header.align"
                                @click="clickOnCheckBoxAll"
                            ></m-checkbox>
                        </template>
                        <template v-else>
                            <el-tooltip
                                v-if="header.fullName"
                                effect="dark"
                                :content="header.fullName"
                                placement="bottom-start"
                            >
                                <div :class="header.align">{{ header.name }}</div>
                            </el-tooltip>
                            <div v-else :class="header.align">
                                {{ header.name }}
                            </div>
                        </template>
                    </div>
                </div>
                <div class="table__content--body" :style="{ height: documentTableHeight + 'px' }">
                    <div
                        v-for="(data, index) in this.transferAssetList"
                        :key="data.Id"
                        class="table__content--row"
                        :class="{
                            'row-selected': isSelected(data),
                            'row-only-selected': this.clickIndex == index,
                        }"
                        @click="clickOnRowTable(index, data.TransferAssetId, $event)"
                        @contextmenu.prevent="showContextMenu($event, index, data.TransferAssetId)"
                        @dblclick="
                            async () => {
                                await this.getTransferAssetDetail(data.TransferAssetId);
                                this.showTransferForm(this.$_MISAEnum.formUpdate);
                            }
                        "
                    >
                        <div class="text-align-center cell--item cell--item--checkbox" style="width: 50px">
                            <m-checkbox
                                :ref="`checkbox-${data.TransferAssetId}`"
                                @click="clickOnCheckbox(index, data.TransferAssetId)"
                            ></m-checkbox>
                        </div>
                        <div class="text-align-center cell--item" style="width: 50px">{{ index + 1 }}</div>
                        <div class="text-align-left cell--item" style="width: 150px">
                            <div class="text--surround">{{ data.TransferAssetCode }}</div>
                        </div>
                        <div class="text-align-center cell--item" style="width: 200px">
                            <div class="text--surround">{{ this.formatedCurrentDate(data.TransactionDate) }}</div>
                        </div>
                        <div class="text-align-center cell--item" style="width: 200px">
                            <div class="text--surround">{{ this.formatedCurrentDate(data.TransferDate) }}</div>
                        </div>
                        <div class="text-align-right cell--item" style="width: 150px">
                            <div class="text--surround">{{ this.formatedMoney(data.OriginalPrice) }}</div>
                        </div>
                        <div class="text-align-right cell--item" style="width: 150px">
                            <div class="text--surround">{{ this.formatedMoney(data.ResidualPrice) }}</div>
                        </div>
                        <div class="text-align-left cell--item" style="flex: 1">
                            <div class="text--surround">{{ data.Note }}</div>
                        </div>
                        <div class="table-list-icons cell--item" style="width: 120px">
                            <div
                                v-tippy="$_MISAResource['vn-VI'].edit"
                                class="table--icon table--icon-pencil"
                                @click.stop
                                @click="
                                    async () => {
                                        await this.getTransferAssetDetail(data.TransferAssetId);
                                        this.showTransferForm(this.$_MISAEnum.formUpdate);
                                    }
                                "
                            ></div>
                            <div
                                v-tippy="$_MISAResource['vn-VI'].delete"
                                @click.stop
                                class="table--icon table--icon-delete"
                                @click="this.deleteTransferAsset(index, data.TransferAssetId)"
                            ></div>
                        </div>
                    </div>
                </div>
                <div class="table__content--sumary">
                    <div class="text-align-center cell--item" style="width: 50px"></div>
                    <div style="width: 50px"></div>
                    <div style="width: 150px"></div>
                    <div style="width: 200px"></div>
                    <div style="width: 200px"></div>
                    <div style="width: 150px; font-weight: bold; padding: 0px 16px" class="text-align-right">
                        {{ this.formatedMoney(this.totalPrice) }}
                    </div>
                    <div style="width: 150px; font-weight: bold; padding: 0px 16px" class="text-align-right">
                        {{ this.formatedMoney(this.totalResidualPrice) }}
                    </div>
                    <div style="flex: 1" class="text-align-right"></div>
                    <div style="width: 120px; font-weight: bold; padding: 0px 16px" class="text-align-right"></div>
                </div>
            </div>
            <div class="table__paging paging" v-if="this.$store.getters.getIsShowPaging">
                <m-pagination
                    :dataSelect="this.numberOfRecordsPerPage"
                    :numberPages="this.pageNumberDocument * 10"
                    @changeCurrentPage="changeCurrentPageDocument"
                    :pageSize="this.pageSizeDocument"
                    :totalRecords="this.totalRecordDocuments"
                    @changePageSize="changePageSizeDocument"
                ></m-pagination>
            </div>
            <div v-if="this.transferAssetList.length == 0" class="table__content--empty">
                <div class="icon--empty"></div>
            </div>
            <div v-if="this.isLoadingDataDocument" class="grid-loading-container">
                <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
                <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
                <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
                <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
                <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
                <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
                <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
                <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
                <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
                <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
                <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
                <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
                <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
                <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
                <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
                <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
                <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
            </div>
        </div>
        <div class="tranfer__separate" ref="separate" @mouseenter="startResize" @mousedown="mousedownHandler"></div>
        <div class="detail__document">
            <div class="detail__header">
                <div class="detail__header--left">
                    <div class="detail__label">Thông tin chi tiết</div>
                </div>
                <div class="detail__header--right">
                    <div class="detail_hidden__icon" v-if="this.isShowDetailDocument" @click="toggleDetailDocument">
                        <div class="icon--arrow-down"></div>
                    </div>
                    <div class="detail_show__icon" v-if="!this.isShowDetailDocument" @click="toggleDetailDocument">
                        <div class="icon--arrow-up"></div>
                    </div>
                </div>
            </div>
            <div class="detail__content" v-if="this.isShowDetailDocument">
                <div class="table__content">
                    <div class="table__content--header">
                        <div
                            class="table__content--header-item cell--item"
                            v-for="(header, index) in this.listHeaderDetailTranfer"
                            :key="index"
                            :style="header.style"
                        >
                            <el-tooltip
                                v-if="header.fullName"
                                effect="dark"
                                :content="header.fullName"
                                placement="bottom-start"
                            >
                                <div :class="header.align">{{ header.name }}</div>
                            </el-tooltip>
                            <div v-else :class="header.align">
                                {{ header.name }}
                            </div>
                        </div>
                    </div>
                    <div class="table__content--body" :style="{ height: documentDetailTableHeight + 'px' }">
                        <div
                            v-for="(data, index) in this.transferAssetDetailList"
                            :key="data.TransferAssetDetailId"
                            class="table__content--row"
                        >
                            <div class="text-align-center cell--item" style="width: 50px">{{ index + 1 }}</div>
                            <div class="text-align-left cell--item" style="width: 120px">
                                <div class="text--surround">{{ data.PropertyCode }}</div>
                            </div>
                            <div class="text-align-left cell--item" style="width: 120px">
                                <div class="text--surround">{{ data.PropertyName }}</div>
                            </div>
                            <div class="text-align-right cell--item" style="width: 120px">
                                <div class="text--surround">{{ this.formatedMoney(data.OriginalPrice) }}</div>
                            </div>
                            <div class="text-align-right cell--item" style="width: 120px">
                                <div class="text--surround">
                                    {{ this.formatedMoney(this.caculateResidualPrice(data)) }}
                                </div>
                            </div>
                            <div class="text-align-left cell--item" style="width: 180px">
                                <div class="text--surround">{{ data.DepartmentName }}</div>
                            </div>
                            <div class="text-align-left cell--item" style="width: 200px">
                                <div class="text--surround">{{ data.DepartmentTransferName }}</div>
                            </div>
                            <div class="text-align-left cell--item" style="flex: 1">
                                <div class="text--surround">{{ data.Reason }}</div>
                            </div>
                        </div>
                    </div>
                </div>
                <div v-if="this.isLoadingDataDetail" class="grid-loading-container">
                    <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
                    <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
                    <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
                    <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
                    <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
                    <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
                    <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
                    <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
                    <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
                    <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
                    <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
                    <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
                    <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
                    <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
                    <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
                    <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
                </div>
            </div>
            <div class="table__paging paging" v-if="this.isShowDetailDocument">
                <m-pagination
                    :dataSelect="this.numberOfRecordsPerPage"
                    :numberPages="this.pageNumbersDetail * 10"
                    @changeCurrentPage="changeCurrentPageDetail"
                    :pageSize="this.pageSizeDocument"
                    :totalRecords="this.totalRecordDetail"
                    @changePageSize="changePageSizeDetail"
                ></m-pagination>
            </div>
        </div>
    </div>
    <PropertyTransferForm
        v-if="this.isShowTransferForm"
        :formMode="this.formMode"
        :transferAssetProps="this.transferAsset"
        @hideTransferForm="hideTransferForm"
        @showToastSuccess="showToastSuccess"
        @createdNewTransferAsset="createdNewTransferAsset"
        @updateTransferAsset="updateTransferAsset"
    ></PropertyTransferForm>
    <m-modal v-if="this.isShowDialog">
        <m-dialog
            :type="this.typeDialog"
            :text="this.textDialog"
            :status="this.statusDialog"
            :documentInfo="this.documentInfoDialog"
            :dialogActions="this.dialogActions"
        ></m-dialog>
    </m-modal>
    <m-toast :label="this.labelToastSuccess" icon="icon--success" v-if="isShowToastSuccess"></m-toast>
    <m-toast :label="this.labelToastError" icon="icon--error" v-if="isShowToastError"></m-toast>
    <m-context-menu
        @showDetail="
            async () => {
                await this.getTransferAssetDetail(this.idSelected);
                this.showTransferForm(this.$_MISAEnum.formUpdate);
            }
        "
        @deleteOneRow="
            () => {
                this.deleteTransferAsset(this.indexSelected, this.idSelected);
            }
        "
        :position="this.contextMenuPosition"
        :isShowContext="this.$store.getters.getIsShowContextMenu"
        :isHasDuplicate="false"
    ></m-context-menu>
</template>

<script scoped>
import PropertyTransferForm from './PropertyTransferForm.vue';
import request from '@/common/api';
import exception from '@/common/exception';
import { formatMoney, formatCurrentDate } from '@/common/common';
import { delay } from '@/common/common';

export default {
    name: 'PropertyTransfer',
    components: {
        PropertyTransferForm,
    },
    data() {
        return {
            totalRecordDocuments: 0,
            totalRecordDetail: 0,
            pageNumbersDetail: 0,
            labelToastSuccess: '',
            labelToastError: '',
            isShowToastSuccess: false,
            isShowToastError: false,
            listHeaderTranfer: this.$_MISAResource['vn-VI'].listHeaderTranfer,
            listHeaderDetailTranfer: this.$_MISAResource['vn-VI'].listHeaderDetailTranfer,
            transferAssetList: [],
            transferAssetDetailList: [],
            documentTableHeight: 189,
            documentDetailTableHeight: 189,
            startMousePositionY: 0,
            isShowDetailDocument: true,
            resizingDetail: false,
            holdingDetail: false,
            isShowTransferForm: false,
            firstClickrow: '',
            selectedRow: [],
            clickIndex: -1,
            currentPageDocument: 1,
            pageSizeDocument: 20,
            detailDocumentPage: 1,
            detailDocumentPageSize: 20,
            numberOfRecordsPerPage: [
                {
                    label: '20',
                    value: '20',
                },
                {
                    label: '40',
                    value: '40',
                },
                {
                    label: '60',
                    value: '60',
                },
            ],
            pageNumberDocument: 0,
            totalPrice: 0,
            totalResidualPrice: 0,
            isLoadingDataDocument: false,
            isLoadingDataDetail: false,
            isShowDialog: false,
            textDialog: '',
            documentInfoDialog: [],
            dialogActions: {},
            formMode: -1,
            transferAsset: null,
            contextMenuPosition: {},
            idSelected: '',
            indexSelected: -1
        };
    },
    watch: {
        detailDocumentPage: async function () {
            await this.getDetailDocument();
        },
        detailDocumentPageSize: async function () {
            await this.getDetailDocument();
        },
        currentPageDocument: async function () {
            await this.getDocumentByPaging();
        },
        pageSizeDocument: async function () {
            await this.getDocumentByPaging();
        },
    },
    async created() {
        await this.getDocumentByPaging();

        this.pageNumberDocument = Math.ceil(this.totalRecordDocuments / this.pageSizeDocument);

        await this.handleGetDetailDocument(0);
    },
    methods: {
        /*
         * Lấy danh sách chi tiết của các chứng từ
         * Author: BATUAN (07/06/2023)
         */
        async getTransferAssetDetail(id) {
            await request
                .getRecord(`TransferAsset/GetInfo?transferAssetId=${id}`)
                .then((res) => {
                    this.transferAsset = res.data;
                })
                .catch((err) => {
                    this.handleException(err.statusCode, err.message, err.documentInfo, this.showDialog);
                });
        },
        /*
         * Gọi api lấy danh sách chứng từ
         * Author: BATUAN (07/06/2023)
         */
        async getDocumentByPaging() {
            this.isLoadingDataDocument = true;
            await request
                .getRecord(
                    `TransferAsset/Paging?pageNumber=${this.currentPageDocument}&pageSize=${Number(
                        this.pageSizeDocument,
                    )}`,
                )
                .then(async (response) => {
                    // lưu dữ liệu data hiển thị
                    this.transferAssetList = response.data.Data;
                    // lưu tổng số bản ghi
                    this.totalRecordDocuments = response.data.Total[0].NumberRecords;
                    // lưu giá trị tổng số
                    this.totalPrice = response.data.Total[0].TotalPrice;
                    if (response.data.Data.length > 0) {
                        this.totalResidualPrice = response.data.Data[0].TotalResidualPrice;
                    }

                    await delay(500);
                    this.isLoadingDataDocument = false;
                })
                .catch((err) => {
                    this.handleException(err.statusCode, err.message, '', this.showDialog);
                });
        },
        /*
         * Lấy ra danh sách các tài sản trong 1 điều chuyển
         * Author: BATUAN (20/08/2023)
         */
        async getDetailDocument() {
            this.isLoadingDataDetail = true;
            await request
                .getRecord(
                    `TransferAssetDetail/FilterByTransferAssetId?assetDetailId=${
                        this.transferAssetList[this.clickIndex].TransferAssetId
                    }&pageNumber=${this.detailDocumentPage}&pageSize=${this.detailDocumentPageSize}`,
                )
                .then(async (response) => {
                    // lưu dữ liệu data hiển thị
                    await delay(500);
                    this.isLoadingDataDetail = false;
                    this.transferAssetDetailList = response.data;
                    this.totalRecordDetail = this.transferAssetDetailList[0].TotalRecords;
                    this.pageNumbersDetail = Math.ceil(this.totalRecordDetail / this.detailDocumentPageSize);
                })
                .catch(() => {
                    // this.handleException(err.statusCode, err.message, this.showDialog);
                });
        },
        /*
         * Ẩn / hiện detail
         * Author: BATUAN (07/06/2023)
         */
        toggleDetailDocument() {
            this.isShowDetailDocument = !this.isShowDetailDocument;
        },
        /*
         * Sự kiện liên quan đến resize kích thước bảng master/detail
         * Author: BATUAN (07/06/2023)
         */
        startResize() {
            if (!this.holdingDetail) {
                this.resizingDetail = true;
            }
        },
        /*
         * Sự kiện liên quan đến resize kích thước bảng master/detail
         * Author: BATUAN (07/06/2023)
         */
        stopResize() {
            if (this.holdingDetail) {
                this.resizingDetail = false;
                this.holdingDetail = false;
                window.removeEventListener('mousemove', this.mousemoveHandler);
                window.removeEventListener('mouseup', this.stopResize);
            }
        },
        /*
         * Sự kiện liên quan đến resize kích thước bảng master/detail
         * Author: BATUAN (07/06/2023)
         */
        mousedownHandler(event) {
            if (this.resizingDetail) {
                this.holdingDetail = true;
                this.startMousePositionY = event.clientY;
                window.addEventListener('mousemove', this.mousemoveHandler);
                window.addEventListener('mouseup', this.stopResize);
            }
        },
        /*
         * Sự kiện liên quan đến resize kích thước bảng master/detail
         * Author: BATUAN (07/06/2023)
         */
        mousemoveHandler(event) {
            if (this.holdingDetail) {
                const deltaY = event.clientY - this.startMousePositionY;
                if (Math.abs(deltaY) > 10) {
                    if (deltaY < 0 && this.documentTableHeight > 10) {
                        this.documentTableHeight += deltaY;
                        this.documentDetailTableHeight -= deltaY;
                        this.startMousePositionY = event.clientY;
                    } else if (deltaY > 0 && this.documentDetailTableHeight > 10) {
                        this.documentTableHeight += deltaY;
                        this.documentDetailTableHeight -= deltaY;
                        this.startMousePositionY = event.clientY;
                    }
                }
            }
        },
        /*
         * Show context menu
         * Author: BATUAN (29/06/2023)
         */
        showContextMenu(event, index, id) {
            this.indexSelected = index;
            this.idSelected = id;
            this.clickIndex = index;

            const browserHeight = window.innerHeight;
            const browserWidth = window.innerWidth;

            // Tính toán vị trí context menu

            const menuWidth = this.$store.getters.getContextMenuSize.x;
            const menuHeight = this.$store.getters.getContextMenuSize.y;
            let positionX = event.clientX;
            let positionY = event.clientY;

            // Kiểm tra xem context menu có bị che khuất không
            if (positionX + menuWidth > browserWidth) {
                positionX = browserWidth - menuWidth;
            }

            if (positionY + menuHeight > browserHeight) {
                positionY = browserHeight - menuHeight;
            }

            this.contextMenuPosition = { x: positionX, y: positionY };

            this.$store.commit('showContextMenu');

            document.addEventListener('click', this.hideContextMenu);

            document.addEventListener('wheel', this.hideContextMenu);

            event.preventDefault();
        },
        /*
         * Show context menu
         * Author: BATUAN (29/06/2023)
         */
        hideContextMenu() {
            this.$store.commit('hideContextMenu');
            document.removeEventListener('click', this.hideContextMenu);
            document.removeEventListener('wheel', this.hideContextMenu);
        },
        /*
         * Hiển thị form chọn tài sản
         * Author: BATUAN (07/06/2023)
         */
        showTransferForm(mode) {
            this.isShowTransferForm = true;
            this.formMode = mode;
        },
        /*
         * Ẩn form chọn tài sản
         * Author: BATUAN (07/06/2023)
         */
        hideTransferForm() {
            this.isShowTransferForm = false;
        },
        /*
         * Sự kiện liên quan đến click vào hàng trong table
         * Author: BATUAN (07/06/2023)
         */
        async handleClickOnRow(index) {
            this.firstClickRow = index;

            await this.handleGetDetailDocument(index);
        },
        /*
         * Lấy lại dữ liệu của detail khi click vào master
         * Author: BATUAN (12/06/2023)
         */
        async handleGetDetailDocument(index) {
            this.clickIndex = index;
            if (this.transferAssetList.length > 0) {
                await this.getDetailDocument();
            }
        },
        /*
         * Sự kiện click vào hàng của table
         * Author: BATUAN (12/06/2023)
         */
        clickOnRowTable(index, id, event) {
            // Nếu là sự kiện double click thì bỏ qua
            if (event.detail == 2) {
                return;
            }
            // Sự kiện Ctrl + Click
            if (event.ctrlKey) {
                if (!this.selectedRow.some((transfer) => transfer.TransferAssetId == id)) {
                    this.selectedRow.push(this.transferAssetList[index]);
                } else {
                    this.selectedRow = this.selectedRow.filter((row) => {
                        return row.TransferAssetId != id;
                    });
                }
            } else if (event.shiftKey) {
                if (this.firstClickRow || this.firstClickRow === 0) {
                    this.secondClickRow = index;

                    this.selectedRow = this.selectedRow.filter((transfer) => {
                        return !this.transferAssetList.some((item) => item.TransferAssetId == transfer.TransferAssetId);
                    });

                    if (this.secondClickRow > this.firstClickRow) {
                        for (let i = this.firstClickRow; i <= this.secondClickRow; i++) {
                            const transfer = this.transferAssetList[i];
                            if (!this.selectedRow.some((item) => item.TransferAssetId == transfer.TransferAssetId)) {
                                this.selectedRow.push(transfer);
                            }
                        }
                    } else {
                        for (let i = this.secondClickRow; i <= this.firstClickRow; i++) {
                            const transfer = this.transferAssetList[i];
                            if (!this.selectedRow.some((item) => item.TransferAssetId == transfer.TransferAssetId)) {
                                this.selectedRow.push(transfer);
                            }
                        }
                    }
                }
            } else {
                this.handleClickOnRow(index, id);
            }
            this.checkForCheckbox();
            this.checkFullChecked();
        },
        /*
         * Check xem 1 hàng có đang được chọn hay không
         * Author: BATUAN (14/06/2023)
         */
        isSelected(transfer) {
            if (this.selectedRow) {
                return this.selectedRow.some((trans) => trans.TransferAssetId == transfer.TransferAssetId);
            }
            return false;
        },

        /*
         * Kiểm tra xem tất cả các hàng trong 1 trang table có được chọn hay không
         * Author: BATUAN (28/06/2023)
         */
        checkFullChecked() {
            let check = true;
            this.transferAssetList.forEach((data) => {
                if (!this.selectedRow.some((item) => item.TransferAssetId == data.TransferAssetId)) {
                    check = false;
                }
            });

            if (check == true && this.transferAssetList.length > 0) {
                this.$refs['checkbox-all'][0].isChecked = true;
            } else {
                this.$refs['checkbox-all'][0].isChecked = false;
            }
        },
        /*
         * Sự kiện liên quan kiểm tra 1 hàng có đang được check không
         * Author: BATUAN (07/06/2023)
         */
        checkForCheckbox() {
            for (let i = 0; i < this.transferAssetList.length; i++) {
                if (
                    this.selectedRow.some((item) => item.TransferAssetId == this.transferAssetList[i].TransferAssetId)
                ) {
                    this.$refs[`checkbox-${this.transferAssetList[i].TransferAssetId}`][0].isChecked = true;
                } else {
                    this.$refs[`checkbox-${this.transferAssetList[i].TransferAssetId}`][0].isChecked = false;
                }
            }
        },
        /*
         * Sự kiện click vào ô checkbox
         * Author: BATUAN (07/06/2023)
         */
        clickOnCheckbox(index, id) {
            if (this.$refs[`checkbox-${id}`][0].isChecked) {
                this.selectedRow = this.selectedRow.filter((item) => {
                    return item.TransferAssetId != id;
                });
            } else {
                this.selectedRow.push(this.transferAssetList[index]);
            }
            this.checkForCheckbox();
            this.checkFullChecked();
        },
        /*
         * Sự kiện click vào ô checkbox all
         * Author: BATUAN (07/06/2023)
         */
        clickOnCheckBoxAll() {
            if (this.$refs['checkbox-all'][0].isChecked) {
                this.$refs['checkbox-all'][0].isChecked = false;
                let transferAssetListId = [];
                this.transferAssetList.forEach((element) => {
                    transferAssetListId.push(element.TransferAssetId);
                });

                this.selectedRow = this.selectedRow.filter((item) => {
                    return !transferAssetListId.includes(item.TransferAssetId);
                });
                this.checkForCheckbox();
            } else if (!this.$refs['checkbox-all'][0].isChecked) {
                for (let i = 0; i < this.transferAssetList.length; i++) {
                    if (
                        !this.selectedRow.some(
                            (item) => item.TransferAssetId == this.transferAssetList[i].TransferAssetId,
                        )
                    ) {
                        this.selectedRow.push(this.transferAssetList[i]);
                    }
                }
                this.checkForCheckbox();
            }
            this.checkFullChecked();
        },
        /*
         * Xử lý mã lỗi backend trả về
         * Author: BATUAN (29/06/2023)
         */
        handleException(statusCode, message, documentInfo, showToastError) {
            exception(statusCode, message, documentInfo, showToastError);
        },
        /*
         * Format giá trị tiền
         * Author: BATUAN (27/05/2023)
         */
        formatedMoney(value) {
            return formatMoney(value);
        },
        /*
         * Format định dạng ngày tháng thành dd-mm-yyyy
         * Author: BATUAN (29/05/2023)
         */
        formatedCurrentDate(date) {
            return formatCurrentDate(date);
        },
        /*
         * Hiển thị dialog
         * Author: BATUAN (29/08/2023)
         */
        showDialog(
            textDialog,
            documentInfo,
            dialogActions = {
                thirdDialogBtnText: 'Đóng',
                thirdBtnFunction: this.hideDialog,
            },
        ) {
            this.isShowDialog = true;
            this.documentInfoDialog = documentInfo;
            this.textDialog = textDialog;
            this.dialogActions = dialogActions;
        },
        /*
         * Đóng dialog
         * Author: BATUAN (29/08/2023)
         */
        hideDialog() {
            this.isShowDialog = false;
        },
        /*
         * Sự kiện khi click vào ô icon xóa ở mỗi hàng của chứng từ
         * Author: BATUAN (29/08/2023)
         */
        deleteTransferAsset(index, id) {
            this.showDialog(
                `Bạn có muốn xóa chứng từ <strong>${this.transferAssetList[index].TransferAssetCode}</strong> không?`,
                '',
                {
                    firstDialogBtnText: 'Hủy',
                    firstBtnFunction: this.hideDialog,
                    thirdDialogBtnText: 'Xóa',
                    thirdBtnFunction: () => this.deleteRecord(id),
                },
            );
        },
        /*
         * Sự kiện khi click vào ô button xóa nhiều khi chọn vào các bản ghi
         * Author: BATUAN (29/08/2023)
         */
        deleteTransferAssets() {
            if (this.selectedRow.length == 1) {
                this.showDialog(
                    `Bạn có muốn xóa chứng từ <strong>${this.selectedRow[0].TransferAssetCode}</strong> không?`,
                    '',
                    {
                        firstDialogBtnText: 'Hủy',
                        firstBtnFunction: this.hideDialog,
                        thirdDialogBtnText: 'Xóa',
                        thirdBtnFunction: this.deleteRecords,
                    },
                );
            } else {
                this.showDialog(
                    `<strong>${this.selectedRow.length}</strong> chứng từ đã được chọn, bạn có muốn xóa các chứng từ này không ?`,
                    '',
                    {
                        firstDialogBtnText: 'Hủy',
                        firstBtnFunction: this.hideDialog,
                        thirdDialogBtnText: 'Xóa',
                        thirdBtnFunction: this.deleteRecords,
                    },
                );
            }
        },
        /*
         * Hàm gọi api xóa chứng từ khi xóa nhiều
         * Author: BATUAN (29/08/2023)
         */
        async deleteRecords() {
            let listId = [];
            this.selectedRow.forEach((row) => {
                listId.push(row.TransferAssetId);
            });
            await request
                .deleteRecord('TransferAsset', { data: listId })
                .then(() => {
                    this.showToastSuccess('Xóa thành công');
                    this.hideDialog();
                    this.getDocumentByPaging();
                    this.selectedRow = [];
                })
                .catch((err) => {
                    this.handleException(err.statusCode, err.message, err.documentInfo, this.showDialog);
                });
        },
        /*
         * Hàm gọi api xóa chứng từ khi click vào ô xóa ở mỗi hàng
         * Author: BATUAN (29/08/2023)
         */
        async deleteRecord(id) {
            let listId = [];
            listId.push(id);
            await request
                .deleteRecord('TransferAsset', { data: listId })
                .then(async () => {
                    this.showToastSuccess('Xóa thành công');
                    this.hideDialog();
                    this.getDocumentByPaging();
                    this.selectedRow = [];
                })
                .catch((err) => {
                    this.handleException(err.statusCode, err.message, err.documentInfo, this.showDialog);
                });
        },
        /*
         * Show toast hiển thị thành công
         * Author: BATUAN (27/05/2023)
         */
        showToastSuccess(label) {
            this.isShowToastSuccess = true;
            this.labelToastSuccess = label;
            // Ẩn sau 3s
            setTimeout(() => {
                this.isShowToastSuccess = false;
            }, 3000);
        },
        /*
         * Gọi api tạo mới chứng từ điều chuyển
         * Author: BATUAN (27/08/2023)
         */
        async createdNewTransferAsset(transferAsset) {
            this.$store.commit('toggleMaskElementShow');
            await request
                .insertRecord('TransferAsset/Insert', transferAsset)
                .then(async () => {
                    await delay(200);
                    this.$store.commit('toggleMaskElementShow');
                    this.showToastSuccess('Thêm chứng từ thành công');
                    this.hideTransferForm();
                    await this.getDocumentByPaging();
                    await this.handleGetDetailDocument(0);
                })
                .catch(async (err) => {
                    await delay(200);
                    this.$store.commit('toggleMaskElementShow');
                    this.handleException(err.statusCode, err.message, err.documentInfo, this.showDialog);
                });
        },
        /*
         * Gọi api update chứng từ điều chuyển
         * Author: BATUAN (27/08/2023)
         */
        async updateTransferAsset(transferAsset) {
            this.$store.commit('toggleMaskElementShow');
            await request
                .updateRecord('TransferAsset', transferAsset)
                .then(async () => {
                    await delay(200);
                    this.$store.commit('toggleMaskElementShow');
                    this.showToastSuccess('Chỉnh sửa chứng từ thành công');
                    this.hideTransferForm();
                    await this.getDocumentByPaging();
                    await this.handleGetDetailDocument(0);
                })
                .catch(async (err) => {
                    await delay(200);
                    this.$store.commit('toggleMaskElementShow');
                    this.handleException(err.statusCode, err.message, err.documentInfo, this.showDialog);
                });
        },
        /*
         * Bỏ chọn hết các ô checkbox
         * Author: BATUAN (27/08/2023)
         */
        removeAllSelected() {
            this.selectedRow = [];
            this.checkFullChecked();
            this.checkForCheckbox();
        },
        /*
         * Tính toán giá trị còn lại của các tài sản
         * Author: BATUAN (30/08/2023)
         */
        caculateResidualPrice(property) {
            const currentDate = new Date();
            return property.OriginalPrice - (currentDate.getFullYear() - property.FollowYear) * property.WearRateValue;
        },
        async changeCurrentPageDetail(newPage) {
            this.detailDocumentPage = newPage;
        },
        async changePageSizeDetail(newPageSize) {
            this.detailDocumentPageSize = newPageSize;
        },
        async changeCurrentPageDocument(newPage) {
            this.currentPageDocument = newPage;
        },
        async changePageSizeDocument(newPageSize) {
            this.pageSizeDocument = newPageSize;
        },
        async handleReload() {
            await this.getDocumentByPaging();

            await this.handleGetDetailDocument(0);
        },
    },
};
</script>
<style scoped>
@import url(../../css/views/property/property-tranfer.css);
@import url(../../css/components/button.css);
@import url(../../css/components/icon.css);
@import url(../../css/components/table.css);
</style>
