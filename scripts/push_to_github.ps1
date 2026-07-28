# Run this script from PowerShell (run as user, not from this service).
# Usage: Open a new PowerShell window, cd to the repository root and run: .\scripts\push_to_github.ps1

param(
	[string]$RemoteUrl = 'https://github.com/Snorlax231155/LMS.git',
	[string]$CommitMessage = 'Initial commit — LMS app and docs'
)

Write-Host "Repository root: $(Get-Location)"

if (-not (Get-Command git -ErrorAction SilentlyContinue)) {
	Write-Error "git is not available in PATH. Install Git and re-open PowerShell: https://git-scm.com/downloads"
	exit 1
}

# Initialize if needed
if (-not (Test-Path .git)) {
	git init
	Write-Host "Initialized empty git repository"
} else {
	Write-Host ".git already exists"
}

# Stage files
git add .

# Commit (if there are staged changes)
$commitExit = 0
try {
	git commit -m $CommitMessage
	$commitExit = $LASTEXITCODE
} catch {
	$commitExit = $LASTEXITCODE
}
if ($commitExit -ne 0) {
	Write-Host "No commit created (possibly nothing to commit) or commit failed. Exit code: $commitExit"
}

# Ensure main branch
git branch -M main

# Set remote
# Remove existing origin if present
try { git remote remove origin 2>$null } catch {}

git remote add origin $RemoteUrl

# Push (may prompt for credentials)
Write-Host "Pushing to $RemoteUrl ..."
try {
	git push -u origin main
} catch {
	Write-Error "Push failed. Ensure you have permission and credentials set up. Consider using 'gh auth login' or setting a PAT in credential manager."
	exit 1
}

Write-Host "Done. If push succeeded, your repository is on GitHub."