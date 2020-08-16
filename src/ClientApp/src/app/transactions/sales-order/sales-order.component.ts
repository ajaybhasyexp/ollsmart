import { Component, OnInit,Inject,ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { NgbModal,NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { DatePipe } from '@angular/common';
import { SalesOrder } from '../../models/SalesOrder';
import { ExpenseHead } from '../../models/ExpenseHead';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import Swal from 'sweetalert2';

import { ExporterService } from '../../services/exporter.service';

@Component({
  selector: 'app-sales-order',
  templateUrl: './sales-order.component.html',
  styleUrls: ['./sales-order.component.css']
})
export class SalesOrderComponent implements OnInit {
  displayedColumns: string[] = ['Date','OrderDate','OrderNo','User','Status','Actions'];
  dataSource: MatTableDataSource<SalesOrder>;
  salesOrder = new SalesOrder();
  salesOrders: Array<SalesOrder> = new Array<SalesOrder>();
  // expenseHeads: Array<ExpenseHead> = new Array<ExpenseHead>();
  modalReference: NgbModalRef;

  dateRangeForm = new FormGroup({
    fromDateControl: new FormControl(new Date(), Validators.required), 
    toDateControl: new FormControl(new Date(), Validators.required)
  });
  
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;
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
  exportToExcel() {
    var filtered = this.salesOrders.map(function(x) { return {'Date':x.orderDate,'Order No':x.orderNo,'User':x.userId, 'Amount':x.totalAmount,'LineItem':x.lineItem}; })
    this.exporterService.exportJsonToExcel(filtered, 'SalesOrder.xlsx',[]);
   }  
  ngOnInit() {
    this.onSubmit();
   }
   getTotalAmount() {
     return this.salesOrders.map(t => t.totalAmount).reduce((acc, value) => acc + value, 0);
   }
  //  getExpenseHeads(){ 
  //    this.http.get<Array<SalesOrder>>(this.baseUrl + 'api/Order/OrderDetails').subscribe(result => {
  //      this.salesOrders =  result;
  //    }, error => console.error(error));
  //  }
   onSubmit(){
     this.btnSubmited = true;
     if (this.dateRangeForm.valid) {
       var fromDate=this.datePipe.transform(this.dateRangeForm.get('fromDateControl').value,"yyyy-MM-dd");
       var toDate=this.datePipe.transform(this.dateRangeForm.get('toDateControl').value,"yyyy-MM-dd");
       this.http.get<Array<SalesOrder>>(this.baseUrl + 'api/Order/OrderDetails/'+fromDate+'/'+toDate+'').subscribe(result => {
         this.salesOrders = result;
         this.dataSource = new MatTableDataSource(this.salesOrders);
         this.dataSource.paginator = this.paginator;
         this.dataSource.sort = this.sort;
       }, error => console.error(error)); 
     }   
   }

}
