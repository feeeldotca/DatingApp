import { Component, OnInit } from '@angular/core';
import { User } from '../models/user.modle';
import { ServiceService } from '../service.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  users: User[]=[];
  constructor(private service: ServiceService){}

  ngOnInit(): void {
    this.service.getUsers().subscribe(response=>{
      this.users=response;
    })
  }

}
