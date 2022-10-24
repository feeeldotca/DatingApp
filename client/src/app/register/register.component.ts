import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model: any = {};
  constructor() { }
  cancelled: Boolean = true;

  ngOnInit(): void {
  }

  register() {
    console.log(this.model);    
  }

  cancel() {
    console.log('cancelled');
    this.cancelled = false;
    
  }

}
