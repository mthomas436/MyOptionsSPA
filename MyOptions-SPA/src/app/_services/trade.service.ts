import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { Trade } from '../_models/trade';
import { Option } from '../_models/option';


@Injectable({
  providedIn: 'root'
})
export class TradeService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }
/*
  getTrades(): Observable<Trade> {
    return this.http.get(this.baseUrl + + 'trades/');
  }
  */
 getTrade(tradeid): Observable<Trade> {
   return this.http.get<Trade>(this.baseUrl + 'trades/gettrade/' + tradeid);
 }

 getTrades(userid): Observable<Trade[]> {
   return this.http.get<Trade[]>(this.baseUrl + 'trades/gettrades/' + userid);
 }


 addTrade(trade: Trade, userid: number) {
  return this.http.post(this.baseUrl + 'trades/createtrade', trade);
 }

 updateTrade(trade: Trade, userid: number) {
  return this.http.post(this.baseUrl + 'trades/updatetrade', trade);
 }


 deleteTrade(tradeid: number) {
  return this.http.delete(this.baseUrl + 'trades/deletetrade/' + tradeid);
 }


 getPosition(tradeid: number): Observable<Option[]> {
  return this.http.get<Option[]>(this.baseUrl + 'options/getoptions/' + tradeid);
 }

 getOption(optionid: number): Observable<Option> {
  return this.http.get<Option>(this.baseUrl + 'options/getoption/' + optionid);
 }

 addPosition(model: Option) {
   return this.http.post(this.baseUrl + 'options/addoption', model);
 }

 updatePosition(option: Option) {
  return this.http.put<Option>(this.baseUrl + 'options/updateoption/', option);
 }

 deletePosition(optionid): Observable<Trade> {
  return this.http.delete<Trade>(this.baseUrl + 'options/deleteoption/' + optionid);
 }


}
