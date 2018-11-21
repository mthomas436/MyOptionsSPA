import { TradeService } from './../../_services/trade.service';
import { Component, OnInit } from '@angular/core';
import { AlertifyService } from '../../_services/alertify.service';
import { Router } from '@angular/router';
import { UserService } from '../../_services/user.service';
import { User } from '../../_models/user';
import { Trade } from '../../_models/trade';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AlertModule } from 'ngx-bootstrap/alert';
import { AuthService } from '../../_services/auth.service';

@Component({
  selector: 'app-trade-list',
  templateUrl: './trade-list.component.html',
  styleUrls: ['./trade-list.component.css']
})
export class TradeListComponent implements OnInit {
  trade: Trade;
  newTrade: Trade;
  users: User[];
  trades: Trade[];
  tradeForm: FormGroup;
  showTradeForm: boolean;
  showTradeList: boolean;
  userid: number;
  mode: string;
  formTitle: string;

  constructor(public tradeService: TradeService, private alertify: AlertifyService, private authService: AuthService,
              private router: Router) { }

  ngOnInit() {

    this.showTradeForm = false;
    this.showTradeList = true;
    this.userid = this.authService.getUserid();
    this.loadTrades();
    this.createTradeForm();
    this.mode = 'add';
    this.formTitle = 'Add New Trade';
  }


  loadTrades() {
    this.tradeService.getTrades(this.userid).subscribe((trades: Trade[]) => {
      this.trades = trades;
    }, error => {
      this.alertify.error(error);
    });
  }

  editTrade(tradeid: number) {
    this.mode = 'edit';
    this.formTitle = 'Edit Trade';
    this.displayTradeForm();
    this.editTradeForm(tradeid);
  }

  deleteTrade(tradeid: number) {

    this.alertify.confirm('Are you sure you want to delete this trade?', () => {
      this.tradeService.deleteTrade(tradeid).subscribe(() => {
        this.alertify.success('Trade deleted');
        window.location.reload();
        }, error => {
          this.alertify.error(error);
        });
    });

  }

  displayAddTradeForm() {
    this.mode = 'add';
    this.formTitle = 'Add New Trade';
    this.displayTradeForm();
  }

  addTrade() {
    if (this.tradeForm.valid) {
      this.newTrade = Object.assign({}, this.tradeForm.value);
      this.newTrade.userid = this.userid;
      this.tradeService.addTrade(this.newTrade, this.userid).subscribe((ntrade: Trade) => {
        this.alertify.success('Trade added successfully');
        // window.location.reload();
        this.loadTrades();
        this.hideTradeForm();
      }, error => {
        this.alertify.error(error);
      });

      this.tradeForm.reset();
    }
  }

  updateTrade() {
    if (this.tradeForm.valid) {
      this.newTrade = Object.assign({}, this.tradeForm.value);
      this.newTrade.userid = this.userid;
      this.tradeService.updateTrade(this.newTrade, this.userid).subscribe((ntrade: Trade) => {
        this.alertify.success('Trade updated successfully');
        this.loadTrades();
        this.hideTradeForm();
      }, error => {
        this.alertify.error(error);
      });
    }
  }

  displayTradeForm() {
    this.showTradeForm = true;
    this.showTradeList = false;
  }

  hideTradeForm() {
    this.showTradeForm = false;
    this.showTradeList = true;
  }


  createTradeForm() {
    this.tradeForm = new FormGroup({
     stockSymbol: new FormControl('', Validators.required),
     tradeTypeId: new FormControl('', Validators.required),
     description: new FormControl('', Validators.required),
     notes: new FormControl(''),
     tradeid: new FormControl()
   });
  }


  editTradeForm(tradeid: number) {
    this.tradeService.getTrade(tradeid).subscribe((trade: Trade) => {
      this.tradeForm.controls['stockSymbol'].setValue(trade.stockSymbol);
      this.tradeForm.controls['tradeTypeId'].setValue(trade.tradeTypeId);
      this.tradeForm.controls['description'].setValue(trade.description);
      this.tradeForm.controls['notes'].setValue(trade.notes);
      this.tradeForm.controls['tradeid'].setValue(trade.tradeid);
    }, error  => {
      this.alertify.error(error);
    });

  }
}
