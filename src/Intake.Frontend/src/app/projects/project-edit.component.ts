import { Project } from "./project.model";
import { ProjectService } from "./project.service";
import { EditorComponent } from "../shared";
import { Router } from "../router";

const template = require("./project-edit.component.html");
const styles = require("./project-edit.component.scss");

export class ProjectEditComponent extends HTMLElement {
    constructor(
		private _projectService: ProjectService = ProjectService.Instance,
		private _router: Router = Router.Instance
		) {
        super();
		this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
		this.onTitleClick = this.onTitleClick.bind(this);
    }

    static get observedAttributes() {
        return ["project-id"];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
		this._bind();
		this._setEventListeners();
    }
    
	private async _bind() {
        this._titleElement.textContent = this.projectId ? "Edit Project": "Create Project";

        if (this.projectId) {
            const project: Project = await this._projectService.getById(this.projectId);                
			this._nameInputElement.value = project.name;  
        } else {
            this._deleteButtonElement.style.display = "none";
        } 	
	}

	private _setEventListeners() {
        this._saveButtonElement.addEventListener("click", this.onSave);
		this._deleteButtonElement.addEventListener("click", this.onDelete);
        this._titleElement.addEventListener("click", this.onTitleClick);
    }

    private disconnectedCallback() {
        this._saveButtonElement.removeEventListener("click", this.onSave);
		this._deleteButtonElement.removeEventListener("click", this.onDelete);
        this._titleElement.removeEventListener("click", this.onTitleClick);
    }

    public async onSave() {
        const project = {
            id: this.projectId,
            name: this._nameInputElement.value
        } as Project;
        
        await this._projectService.add(project);
		this._router.navigate(["project","list"]);
    }

    public async onDelete() {        
        await this._projectService.remove({ id: this.projectId });
		this._router.navigate(["project","list"]);
    }

	public onTitleClick() {
        this._router.navigate(["project", "list"]);
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "project-id":
                this.projectId = newValue;
				break;
        }        
    }

    public projectId: number;
    
	private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".project-name") as HTMLInputElement;}
}

customElements.define(`ce-project-edit`,ProjectEditComponent);
