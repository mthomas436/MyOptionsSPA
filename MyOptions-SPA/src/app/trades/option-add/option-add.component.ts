import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { Option } from 'src/app/_models/option';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { TradeService } from 'src/app/_services/trade.service';

@Component({
  selector: 'app-option-add',
  templateUrl: './option-add.component.html',
  styleUrls: ['./option-add.component.css']
})
export class OptionAddComponent implements OnInit {

  constructor(private alertify: AlertifyService, private tradeService: TradeService) { }
  @Input() TradeId: number;
  @Output() CloseAddWindow = new EventEmitter<Boolean>();
  option: Option;
  optionForm: FormGroup;
  tradeid: number;
  newOption: Option;

  ngOnInit() {
    this.createTradeForm();
    this.tradeid = this.TradeId;

  }

  createTradeForm() {
    this.optionForm = new FormGroup({
     strikePrice: new FormControl('', [Validators.required, Validators.pattern('^[0-9].*$')]),
     expirationDate: new FormControl('', Validators.required),
     optionTypeId: new FormControl('', Validators.required),
     quantity: new FormControl('', [Validators.required, Validators.pattern('^[0-9].*')]),
     entryPrice: new FormControl('', [Validators.required, Validators.pattern('^[0-9].*$')]),
     commission: new FormControl('', [Validators.pattern('^[0-9].*$')]),
     stockPriceatPurchace: new FormControl('0.00', [Validators.pattern('^[0-9].*$')]),
     notes: new FormControl(),
     tradeid: new FormControl()
    });
  }

  addOption() {
    if (this.optionForm.valid) {
      this.option = Object.assign({}, this.optionForm.value);
      this.option.tradeid = this.tradeid;

      this.tradeService.addPosition(this.option).subscribe((option: Option) => {
        this.alertify.success('Option added successfully');
        this.newOption = option;
        this.closeAddForm(true);
      }, error => {
        this.alertify.error(error);
      });

    }
  }

  closeAddForm(reload: Boolean) {
      this.CloseAddWindow.emit(reload);

  }

}
