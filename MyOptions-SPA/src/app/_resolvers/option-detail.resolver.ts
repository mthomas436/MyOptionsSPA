
import { catchError } from 'rxjs/operators';
import { AlertifyService } from '../_services/alertify.service';
import { Injectable } from '@angular/core';
import { Resolve, ActivatedRoute, ActivatedRouteSnapshot } from '@angular/router';
import { Trade } from '../_models/trade';
import { Option } from './../_models/option';
import { Router } from '@angular/router';
import { TradeService } from '../_services/trade.service';
import { Observable, of } from 'rxjs';

@Injectable()
export class OptionDetailResolver implements Resolve<Trade> {
  constructor(private tradeService: TradeService, private router: Router,
              private alertify: AlertifyService) {}

  resolve(route: ActivatedRouteSnapshot): Observable<Trade> {
    return this.tradeService.getTrade(route.params['tradeid']).pipe(
      catchError(error => {
        this.alertify.error('Problem retrieving data');
        this.router.navigate(['/trades']);
        return of(null);
      })
    );
  }
}
