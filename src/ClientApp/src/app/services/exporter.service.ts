import { Injectable } from '@angular/core';
import * as xlsx from 'xlsx';

@Injectable({
  providedIn: 'root'
})
export class ExporterService {

  constructor() { }
  
  exportJsonToExcel(data: any[], excelFileName: string,headerInfo: string[]): void {

    const ws: xlsx.WorkSheet =   
    xlsx.utils.json_to_sheet(data,{header:headerInfo}); 
    const wb: xlsx.WorkBook = xlsx.utils.book_new();
    xlsx.utils.book_append_sheet(wb, ws, 'Sheet1');
    xlsx.writeFile(wb,excelFileName); 
  }

  exportTableToExcel(data: any[], excelFileName: string,headerInfo: string[]): void {
    //data=this.epltable.nativeElement;
    const ws: xlsx.WorkSheet =   
    xlsx.utils.table_to_sheet(data); 
    const wb: xlsx.WorkBook = xlsx.utils.book_new();
    xlsx.utils.book_append_sheet(wb, ws, 'Sheet1');
    xlsx.writeFile(wb,excelFileName); 
  }
}
 