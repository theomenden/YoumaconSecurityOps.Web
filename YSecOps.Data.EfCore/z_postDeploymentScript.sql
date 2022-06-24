Begin Tran DeploymentTransaction
set xAct_Abort On;
Set NoCount On;
Set Identity_Insert Pronouns On;
Exec p_AddPronouns;
Set Identity_Insert Pronouns Off;
Set Identity_Insert StaffTypes On;
Exec p_AddStaffTypes;
Set Identity_Insert StaffTypes Off;
Set Identity_Insert Roles On;
Exec p_AddStaffRoles;
Set Identity_Insert Roles Off;
Exec p_AddStartingLocations;
Commit Tran;