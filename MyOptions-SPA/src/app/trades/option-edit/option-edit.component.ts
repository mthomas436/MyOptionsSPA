import { Component, OnInit, TemplateRef } from '@angular/core';
import { Option } from 'src/app/_models/option';
import { Trade } from 'src/app/_models/trade';
import { TradeService } from 'src/app/_services/trade.service';
import { AuthService } from 'src/app/_services/auth.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';


@Component({
  selector: 'app-option-edit',
  templateUrl: './option-edit.component.html',
  styleUrls: ['./option-edit.component.css']
})

export class OptionEditComponent implements OnInit {
  option: Option;
  tradeForm: FormGroup;
  tradeId: number;

  constructor( private tradeService: TradeService,
    private authService: AuthService,
    private alertify: AlertifyService,
    private route: ActivatedRoute,
    private router: Router
    ) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.option = data['option'];
      this.tradeId = this.option.tradeid;
    });

    this.createTradeForm();
  }

  createTradeForm() {
    this.tradeForm = new FormGroup({
     strikePrice: new FormControl('', [Validators.required, Validators.pattern('^[0-9].*$')]),
     expirationDate: new FormControl('', Validators.required),
     optionTypeId: new FormControl('', Validators.required),
     quantity: new FormControl('', [Validators.required, Validators.pattern('^[0-9].*')]),
     entryPrice: new FormControl('', [Validators.required, Validators.pattern('^[0-9].*$')]),
     exitPrice: new FormControl('', Validators.pattern('^[0-9].*$')),
     commission: new FormControl('0.00', [Validators.pattern('^[0-9].*$')]),
     stockPriceatPurchace: new FormControl('0.00', [Validators.pattern('^[0-9].*$')]),
     optionid: new FormControl(),
     notes: new FormControl(),
     opttypetext: new FormControl(),
     totalcost: new FormControl(),
     profitloss: new FormControl(),
     dateclosed: new FormControl()
    });
    this.setFormData();
  }

  setFormData() {

      this.tradeForm.controls['strikePrice'].setValue(this.option.strikePrice);
      this.tradeForm.controls['expirationDate'].setValue(this.option.expDateShort);
      this.tradeForm.controls['optionTypeId'].setValue(this.option.optionTypeId);
      this.tradeForm.controls['quantity'].setValue(this.option.quantity);
      this.tradeForm.controls['entryPrice'].setValue(this.option.entryPrice);
      this.tradeForm.controls['exitPrice'].setValue(this.option.exitPrice);
      this.tradeForm.controls['commission'].setValue(this.option.commission);
      this.tradeForm.controls['optionid'].setValue(this.option.optionid);
      this.tradeForm.controls['notes'].setValue(this.option.notes);
      this.tradeForm.controls['stockPriceatPurchace'].setValue(+this.option.stockPriceatPurchace);
  }


  updateOption() {
    if (this.tradeForm.valid) {
      this.option = Object.assign({}, this.tradeForm.value);
       if (+this.option.exitPrice > 0) {
          this.alertify.confirm('Setting an Exit Price will close this position. Is this OK?', () => {
            this.sendUpdateToServer(this.option);
          });
       } else {
        this.sendUpdateToServer(this.option);
      }
    }
  }

  sendUpdateToServer(option: Option) {
    this.tradeService.updatePosition(option).subscribe((opt: Option) => {
      this.alertify.success('Option updated successfully');
      this.closeEditForm();
    }, error => {
      this.alertify.error(error);
    });
  }

  closeEditForm() {
    this.router.navigate(['/optionlist/' + this.tradeId]);
  }


}




/*

import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { Option } from 'src/app/_models/option';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { TradeService } from 'src/app/_services/trade.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-option-edit',
  templateUrl: './option-edit.component.html',
  styleUrls: ['./option-edit.component.css']
})
export class OptionEditComponent implements OnInit {

  //@Input() OptionData: Option;
 // @Output() CloseEditWindow = new EventEmitter<Boolean>();

  option: Option;
  optionid: number;
  tradeForm: FormGroup;
  optionData: Option;
  private route: ActivatedRoute;

  constructor(private alertify: AlertifyService,
              private tradeService: TradeService,
              private router: Router) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.option = data['option'];
    });
    console.log(this.option);
    //this.createTradeForm();

    console.log(this.option);
  }

  loadOption() {
    this.tradeService.getTrades(this.userid).subscribe((trades: Trade[]) => {
      this.trades = trades;
    }, error => {
      this.alertify.error(error);
    });
  }

  closeEditForm(reload: Boolean) {
    this.CloseEditWindow.emit(reload);
  }


  createTradeForm() {
    this.tradeForm = new FormGroup({
     strikePrice: new FormControl('', [Validators.required, Validators.pattern('^[0-9].*$')]),
     expirationDate: new FormControl('', Validators.required),
     optionTypeId: new FormControl('', Validators.required),
     quantity: new FormControl('', [Validators.required, Validators.pattern('^[0-9].*')]),
     entryPrice: new FormControl('', [Validators.required, Validators.pattern('^[0-9].*$')]),
     exitPrice: new FormControl('', Validators.pattern('^[0-9].*$')),
     commission: new FormControl('0.00', [Validators.pattern('^[0-9].*$')]),
     stockPriceatPurchace: new FormControl('0.00', [Validators.pattern('^[0-9].*$')]),
     optionid: new FormControl(),
     notes: new FormControl(),
     opttypetext: new FormControl(),
     totalcost: new FormControl(),
     profitloss: new FormControl(),
     dateclosed: new FormControl()
    });
    //this.setFormData();
  }

  setFormData() {

      this.tradeForm.controls['strikePrice'].setValue(this.option.strikePrice);
      this.tradeForm.controls['expirationDate'].setValue(this.option.expDateShort);
      this.tradeForm.controls['optionTypeId'].setValue(this.option.optionTypeId);
      this.tradeForm.controls['quantity'].setValue(this.option.quantity);
      this.tradeForm.controls['entryPrice'].setValue(this.option.entryPrice);
      this.tradeForm.controls['exitPrice'].setValue(this.option.exitPrice);
      this.tradeForm.controls['commission'].setValue(this.option.commission);
      this.tradeForm.controls['optionid'].setValue(this.option.optionid);
      this.tradeForm.controls['notes'].setValue(this.option.notes);
      this.tradeForm.controls['stockPriceatPurchace'].setValue(+this.option.stockPriceatPurchace);
  }



  updatePosition() {
    if (this.tradeForm.valid) {
      this.option = Object.assign({}, this.tradeForm.value);
       if (+this.option.exitPrice > 0) {
          this.alertify.confirm('Setting an Exit Price will close this position. Is this OK?', () => {
            this.sendUpdateToServer(this.option);
          });
       } else {
        this.sendUpdateToServer(this.option);
      }
      this.closeEditForm(true);
    }
  }



  sendUpdateToServer(option: Option) {
    this.tradeService.updatePosition(option).subscribe((opt: Option) => {
      this.alertify.success('Option updated successfully');
    }, error => {
      this.alertify.error(error);
    });
  }

}

*/