import { Component, OnInit, Input } from '@angular/core';
import { Option } from 'src/app/_models/option';

@Component({
  selector: 'app-option-detail',
  templateUrl: './option-detail.component.html',
  styleUrls: ['./option-detail.component.css']
})
export class OptionDetailComponent implements OnInit {
  @Input() OptionData: Option;
  option: Option;

  constructor() { }

  ngOnInit() {
    this.setOptionData();
  }

  setOptionData() {
    this.option = this.OptionData;
  }

}
