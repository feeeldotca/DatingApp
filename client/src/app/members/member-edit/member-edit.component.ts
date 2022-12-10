import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { Member } from 'src/app/models/member';
import { User } from 'src/app/models/user';
import { AccountService } from 'src/app/_service/account.service';
import { MembersService } from 'src/app/_service/members.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})

export class MemberEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm | undefined;
    //To prevent member leave Edit profile page to other site without saving changes
  @HostListener('window:beforeunload',['$event']) unloadNotification($event: any){
    if(this.editForm?.dirty) {
      $event.returnValue = true;
    }
  }

    member: Member | undefined;
  user: User | null = null;
  
  constructor(private accountService: AccountService, 
    private toastr: ToastrService,
    private memberService: MembersService) 
  {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user=>this.user = user
    })
   }

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember() {
    if(!this.user) return;
    this.memberService.getMember(this.user.username).subscribe({
      next: member=>this.member = member
    })
  }

  updateMember() {
    //console.log(this.member);
    this.memberService.updateMember(this.editForm?.value)
    this.toastr.success("profile updated successfully!");
    this.editForm?.reset(this.member);
  }

}
