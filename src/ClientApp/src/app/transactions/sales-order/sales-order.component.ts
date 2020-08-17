import { Component, OnInit,Inject,ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { NgbModal,NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { DatePipe } from '@angular/common';
import { OrderHeader } from '../../models/OrderHeader';
import { OrderDetail } from '../../models/OrderDetail';
import { ExpenseHead } from '../../models/ExpenseHead';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import Swal from 'sweetalert2';

import { ExporterService } from '../../services/exporter.service';  
import { SalesOrder } from 'src/app/models/SalesOrder';

@Component({
  selector: 'app-sales-order',
  templateUrl: './sales-order.component.html',
  styleUrls: ['./sales-order.component.css']
})
export class SalesOrderComponent implements OnInit {
  displayedColumns: string[] = ['Date','OrderNo','User','Status','DeliveryDate','TotalAmount','LineItemCount','Actions'];
  dataSource: MatTableDataSource<OrderHeader>;
  displayedColumnsOD: string[] = ['ProductName','PropertyValue','Rate','Quantity','Discount','Amount'];
  dataSourceOD: MatTableDataSource<OrderDetail>;
  orderHeader = new OrderHeader();
  orderHeaders: Array<OrderHeader> = new Array<OrderHeader>();
  orderDetail = new OrderDetail();
  orderDetails: Array<OrderDetail> = new Array<OrderDetail>();
  salesOrders: Array<SalesOrder> = new Array<SalesOrder>();
  modalReference: NgbModalRef;

  dateRangeForm = new FormGroup({
    fromDateControl: new FormControl(new Date(), Validators.required), 
    toDateControl: new FormControl(new Date(), Validators.required)
  });
  
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  @ViewChild(MatPaginator, {static: true}) paginatorOD: MatPaginator;
  @ViewChild(MatSort, {static: true}) sortOD: MatSort;

  public btnSubmited = false;

  baseUrl: string;
  constructor(  private http: HttpClient, 
    @Inject('BASE_URL') url: string,
   public dialog: MatDialog,
   private exporterService: ExporterService,
   private modalService: NgbModal,
   private datePipe: DatePipe) { 
      this.baseUrl=url;
 }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
  applyFilterOD(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSourceOD.filter = filterValue.trim().toLowerCase();
  
    if (this.dataSourceOD.paginator) {
      this.dataSourceOD.paginator.firstPage();
    }
  }
  exportToExcel() {
    var filtered = this.orderHeaders.map(function(x) { return {'Date':x.orderDate,'Order No':x.orderNo,'User':x.userId, 'Status':x.status,'Amount':x.totalAmount,'Delivery Date':x.expectedDeliveryDate,'LineItem':x.lineItemCount}; })
    this.exporterService.exportJsonToExcel(filtered, 'SalesOrder.xlsx',[]);
   }  
  ngOnInit() {
    this.onSubmit();
   }
   getTotalAmount() { 
     return this.orderHeaders.map(t => t.totalAmount).reduce((acc, value) => acc + value, 0);
   }
   getODTotalAmount() {
    return this.orderDetails.map(t => t.amount).reduce((acc, value) => acc + value, 0);
  }
  getODTotalDiscount() {
    return this.orderDetails.map(t => t.discount).reduce((acc, value) => acc + value, 0);
  }
   openModal(content,id:number){
    // this.expense = new Expense();   
    // this.getExpenseHeads();
    if(id>0)
    {
        this.http.get <Array<OrderDetail>>(this.baseUrl + 'api/Order/OrderDetailsById/'+id).subscribe(result => {
          this.orderDetails=result;
          this.dataSourceOD = new MatTableDataSource(result);
          console.log(this.dataSourceOD);
          // this.dataSourceOD.paginator = this.paginatorOD;
          // this.dataSourceOD.sort = this.sortOD;
      }, error => console.error(error));
    }
    // else{
    //   this.clearForm();
    // }
    this.modalReference=this.modalService.open(content,{ size: 'xl' });
  }
  
   onSubmit(){
     this.btnSubmited = true;
     if (this.dateRangeForm.valid) {
       this.orderHeaders =new Array<OrderHeader>();
       var fromDate=this.datePipe.transform(this.dateRangeForm.get('fromDateControl').value,"yyyy-MM-dd");
       var toDate=this.datePipe.transform(this.dateRangeForm.get('toDateControl').value,"yyyy-MM-dd");
       this.http.get<Array<OrderHeader>>(this.baseUrl + 'api/Order/OrderDetails/'+fromDate+'/'+toDate+'').subscribe(result => {
         result.forEach(o =>{
          this.orderHeader = new OrderHeader();
          this.orderHeader=o;
          var total=0;
          o.orderDetail.forEach(od =>{
            total+=od.amount
          });
          this.orderHeader.totalAmount=total;
          this.orderHeader.lineItemCount=o.orderDetail.length;
          this.orderHeaders.push(this.orderHeader);
         });
         console.log(this.orderHeaders);
         this.dataSource = new MatTableDataSource(this.orderHeaders);
         this.dataSource.paginator = this.paginator;
         this.dataSource.sort = this.sort;

       }, error => console.error(error)); 
     }  
   }
   

}
