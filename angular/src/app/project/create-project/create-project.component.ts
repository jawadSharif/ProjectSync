import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/app-component-base';
import { Component, Injector } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
  AsanaServiceProxy,
  StringStringKeyValuePair,
  DevOpsServiceProxy,
  ProjectServiceProxy,
  CreateProjectDto
} from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: './create-project.component.html',
  animations: [appModuleAnimation()]
})
export class CreateProjectComponent extends AppComponentBase {

  asanaToken = '';
  workspace = ''
  asanaProjectId = '';
  asanaWorkSpace = '';
  asanaWorkspaces: StringStringKeyValuePair[] = [];
  asanaProjects: StringStringKeyValuePair[] = [];

  devopsToken = '';
  devopsAccessToken = '';
  devopsOrganization = '';
  devOpsProjectId: string;
  devopsProjects: StringStringKeyValuePair[];

  isSaving = false;

  constructor(
    injector: Injector,
    private _asanaService: AsanaServiceProxy,
    private _devOpsService: DevOpsServiceProxy,
    private _projectService: ProjectServiceProxy,
  ) {
    super(injector);
  }

  refreshAsanaWorkspaces() {
    this._asanaService.getAsanaWorkSpaces(this.asanaToken).subscribe(result => {
      this.asanaWorkspaces = result;
    })
  }

  refrehAsanaTasks() {
    if (!this.workspace)
      return;
    this._asanaService.getAllProjectInWorkSpace(this.workspace, this.asanaToken).subscribe(result => {
      this.asanaProjects = result;
    })
  }

  async refreshDevOpsProjects() {
    let accessToken = await this._devOpsService.accessToken(this.devopsToken).toPromise();
    this.devopsAccessToken = accessToken.item1;
    this._devOpsService.getProjects(this.devopsOrganization, accessToken.item1, '6.0').subscribe(result => {
      this.devopsProjects = result;
    })
  }

  save(event: boolean) {
    this.isSaving = true;
    let body = new CreateProjectDto();
    body.asanaProjectName = this.asanaProjects.find(a => a.key == this.asanaProjectId).value;
    body.asanaProjectId = this.asanaProjectId;
    body.asanaToken = this.asanaToken;
    //body.asanaWorkSpace = this.asanaWorkspaces.find(a => a.key == this.workspace).value;
    body.asanaWorkSpace = this.workspace;

    body.devOpsAccessToken = this.devopsAccessToken;
    body.devOpsOrganization = this.devopsOrganization;
    body.devOpsProject = this.devopsProjects.find(a => a.key == this.devOpsProjectId).value;
    body.shouldCreateAsanaTask = event;
    this._projectService.create(body).pipe(
      finalize(() => {
        this.isSaving = false;
      })
    ).subscribe(result => {
      abp.notify.info('saved successfully');
    })
  }

}
