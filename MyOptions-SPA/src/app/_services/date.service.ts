import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DatePipe } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class DateService {

constructor(private http: HttpClient, private datePipe: DatePipe) { }

 transformDate(date) {
  this.datePipe.transform(date, 'MM/dd/yyyy');
 }

}

