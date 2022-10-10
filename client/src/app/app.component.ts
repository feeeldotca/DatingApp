import { Component, OnInit } from '@angular/core';
import { User } from './models/user.modle';
import { ServiceService } from './service.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'client';
  users: User[]=[];
  constructor(private service: ServiceService){}
  ngOnInit(): void {
    this.service.getUsers().subscribe(response=>{
      this.users=response;
    })
  }
  
}
