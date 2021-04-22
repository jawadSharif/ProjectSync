import { Component, Injector, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
  PagedListingComponentBase,
  PagedRequestDto
} from 'shared/paged-listing-component-base';
import {
  UserServiceProxy,
  UserDto,
  UserDtoPagedResultDto,
  ProjectDto,
  ProjectServiceProxy
} from '@shared/service-proxies/service-proxies';
import { CreateUserDialogComponent } from '@app/users/create-user/create-user-dialog.component';
import { EditUserDialogComponent } from '@app/users/edit-user/edit-user-dialog.component';
import { ResetPasswordDialogComponent } from '@app/users/reset-password/reset-password.component';
import { CreateProjectComponent } from './create-project/create-project.component';
import { Route } from '@angular/compiler/src/core';
import { Router } from '@angular/router';



class PagedUsersRequestDto extends PagedRequestDto {
  keyword: string;
  isActive: boolean | null;
}

@Component({
  templateUrl: './project.component.html',
  animations: [appModuleAnimation()]
})
export class ProjectComponent extends PagedListingComponentBase<ProjectDto> {

  projects: ProjectDto[] = [];

  constructor(
    injector: Injector,
    private _userService: UserServiceProxy,
    private _modalService: BsModalService,
    private _projectService: ProjectServiceProxy,
    private route: Router
  ) {
    super(injector);
  }


  createProject(): void {
    this.route.navigate["/app/create-project"];
  }

  protected list(
    request: PagedUsersRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    this._projectService.getAll('', request.skipCount, request.maxResultCount)
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      ).subscribe(r => {
        this.projects = r.items;
        this.showPaging(r, pageNumber)
      });
  }

  protected delete(user: ProjectDto): void {
    //   abp.message.confirm(
    //     this.l('UserDeleteWarningMessage', user.fullName),
    //     undefined,
    //     (result: boolean) => {
    //       if (result) {
    //         this._userService.delete(user.id).subscribe(() => {
    //           abp.notify.success(this.l('SuccessfullyDeleted'));
    //           this.refresh();
    //         });
    //       }
    //     }
    //   );
  }
}
