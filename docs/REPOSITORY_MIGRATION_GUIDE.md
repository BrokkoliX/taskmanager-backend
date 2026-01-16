# Repository Migration Guide

> **Step-by-step guide to push the TaskManager Backend to a separate Git repository**

This document provides instructions for migrating the backend code to a dedicated repository for the Tabnine demo.

---

## 📋 Prerequisites

- Git installed
- GitHub/GitLab/Azure DevOps account
- SSH keys configured (recommended) or HTTPS credentials
- Access to create repositories in your organization

---

## 🚀 Migration Steps

### **Step 1: Create Remote Repository**

#### Option A: GitHub

```bash
# Using GitHub CLI
gh repo create your-org/taskmanager-backend --public --description "Clean Architecture .NET 9 Backend API"

# Or create manually at: https://github.com/new
```

#### Option B: Azure DevOps

```bash
# Using Azure DevOps CLI
az repos create --name taskmanager-backend --project YourProject

# Or create manually in Azure DevOps Portal
```

#### Option C: GitLab

```bash
# Using GitLab CLI
glab repo create your-org/taskmanager-backend --public

# Or create manually at: https://gitlab.com/projects/new
```

---

### **Step 2: Prepare Backend for Migration**

Navigate to your TaskManager.Api directory:

```bash
cd /Users/robbie/Tab/TabnineTaskDemo/TaskManager.Api
```

---

### **Step 3: Initialize Git Repository** (If not already initialized)

```bash
# Initialize git
git init

# Copy the backend gitignore
cp .gitignore.backend .gitignore

# Verify gitignore is working
git status
```

---

### **Step 4: Stage Backend Files**

```bash
# Add all backend files
git add src/
git add Controllers/
git add docs/
git add TaskManager.Backend.sln
git add TaskManager.Api.csproj
git add Program.cs
git add appsettings.json
git add wwwroot/
git add README.backend.md
git add .gitignore

# Review what will be committed
git status
```

---

### **Step 5: Create Initial Commit**

```bash
# Create initial commit
git commit -m "feat: Initial commit - Clean Architecture Backend

- TaskManager.Core (Domain Layer)
- TaskManager.Infrastructure (Data Layer)
- TaskManager.Application (Business Logic)
- TaskManager.Api (Presentation Layer)
- Clean Architecture implementation
- SQLite database support
- Swagger/OpenAPI documentation
- RESTful API endpoints for Tasks and Users
"
```

---

### **Step 6: Add Remote Origin**

```bash
# Add your remote repository
# Replace with your actual repository URL

# For GitHub (SSH):
git remote add origin git@github.com:your-org/taskmanager-backend.git

# For GitHub (HTTPS):
git remote add origin https://github.com/your-org/taskmanager-backend.git

# For Azure DevOps:
git remote add origin https://dev.azure.com/your-org/YourProject/_git/taskmanager-backend

# For GitLab:
git remote add origin git@gitlab.com:your-org/taskmanager-backend.git

# Verify remote was added
git remote -v
```

---

### **Step 7: Rename Default Branch** (Optional but recommended)

```bash
# Rename master to main (if needed)
git branch -M main
```

---

### **Step 8: Push to Remote Repository**

```bash
# Push to remote repository
git push -u origin main

# Verify push was successful
git log --oneline
```

---

### **Step 9: Create Initial Release Tag**

```bash
# Create and push version tag
git tag -a v1.0.0 -m "Initial release - TaskManager Backend v1.0.0

Features:
- Clean Architecture implementation
- Task management API
- User management API
- SQLite database
- Swagger documentation
- RESTful endpoints
"

# Push tags to remote
git push --tags
```

---

### **Step 10: Update README** (If needed)

```bash
# Rename the backend README to main README
mv README.backend.md README.md

# Commit the change
git add README.md
git commit -m "docs: Update main README"
git push origin main
```

---

### **Step 11: Set Up Repository Settings**

#### Configure Branch Protection (GitHub)

```bash
# Using GitHub CLI
gh api repos/your-org/taskmanager-backend/branches/main/protection \
  --method PUT \
  --field required_status_checks='{"strict":true,"contexts":[]}' \
  --field enforce_admins=true \
  --field required_pull_request_reviews='{"required_approving_review_count":1}'
```

Or configure manually in repository settings:
- Branch protection rules for `main`
- Require pull request reviews
- Require status checks to pass

---

### **Step 12: Add Repository Description & Topics**

#### GitHub

```bash
gh repo edit your-org/taskmanager-backend \
  --description "Clean Architecture .NET 9 Backend API for Task Management" \
  --add-topic "dotnet" \
  --add-topic "clean-architecture" \
  --add-topic "rest-api" \
  --add-topic "sqlite" \
  --add-topic "entity-framework-core"
```

---

### **Step 13: Optional - Create Initial Issues/Projects**

```bash
# Create initial issues for roadmap items
gh issue create --title "Add JWT Authentication" --body "Implement JWT-based authentication for API endpoints"
gh issue create --title "Add API Versioning" --body "Implement API versioning strategy"
gh issue create --title "Add Redis Caching" --body "Integrate Redis for distributed caching"
gh issue create --title "Add Rate Limiting" --body "Implement rate limiting middleware"
gh issue create --title "Add Integration Tests" --body "Create comprehensive integration test suite"
```

---

## 🔍 Verification Checklist

After migration, verify the following:

- [ ] Repository is accessible at the remote URL
- [ ] All source code files are present
- [ ] `.gitignore` is working (bin/, obj/, database files excluded)
- [ ] README.md displays correctly
- [ ] Solution builds successfully after fresh clone
- [ ] Documentation is accessible
- [ ] Version tag `v1.0.0` exists
- [ ] Branch protection rules are active (if configured)
- [ ] Repository description and topics are set

---

## 🧪 Test Fresh Clone

To verify the migration was successful, clone the repository in a new directory:

```bash
# Clone to a new directory
cd ~/test-clone
git clone https://github.com/your-org/taskmanager-backend.git
cd taskmanager-backend

# Restore dependencies
dotnet restore

# Build solution
dotnet build TaskManager.Backend.sln

# Run the API
cd src/TaskManager.Api
dotnet run

# Verify API is running
curl http://localhost:5000/swagger
```

---

## 📁 What Gets Excluded

The `.gitignore` file excludes:

- ✅ `bin/` and `obj/` (build artifacts)
- ✅ `*.db` and `*.sqlite` (database files)
- ✅ `.env` files (environment secrets)
- ✅ `.vs/` and `.vscode/` (IDE settings)
- ✅ User-specific files
- ✅ Build logs and temporary files
- ✅ NuGet packages (restored via `dotnet restore`)

---

## 🔄 Keeping Repositories in Sync

If you're maintaining both the original and backend-only repositories:

```bash
# In the original repository, add backend repo as a remote
git remote add backend git@github.com:your-org/taskmanager-backend.git

# Push changes to backend
git subtree push --prefix=. backend main
```

Or use separate workflows for each repository.

---

## 🚨 Troubleshooting

### Issue: Large files rejected

```bash
# If you accidentally committed large files
git filter-branch --tree-filter 'rm -f path/to/large/file' HEAD

# Or use git-filter-repo (recommended)
git filter-repo --path path/to/large/file --invert-paths
```

### Issue: Authentication failed

```bash
# For HTTPS, cache credentials
git config --global credential.helper cache

# Or switch to SSH
git remote set-url origin git@github.com:your-org/taskmanager-backend.git
```

### Issue: .gitignore not working

```bash
# Clear git cache
git rm -r --cached .
git add .
git commit -m "fix: Update .gitignore"
```

---

## 📚 Next Steps

After successful migration:

1. **Set up CI/CD Pipeline**
   - GitHub Actions
   - Azure DevOps Pipelines
   - GitLab CI

2. **Configure Code Quality Tools**
   - SonarQube
   - CodeCov
   - Dependabot

3. **Add Documentation**
   - API documentation
   - Architecture diagrams
   - Contributing guidelines

4. **Set Up Development Environment**
   - Docker containerization
   - Development containers
   - Local development guide

---

## 🔗 Related Documents

- [Backend Architecture Guide](README_BACKEND.md)
- [Architecture Migration Plan](../ARCHITECTURE_MIGRATION_PLAN.md)
- [Contributing Guidelines](CONTRIBUTING.md) (create if needed)

---

## ✅ Migration Complete!

Once all steps are completed and verified, your backend repository is ready for:
- Team collaboration
- CI/CD integration
- Tabnine remote repository indexing
- Independent deployment
- Separate release cycles

---

**Repository Migration Date:** [Add date when completed]  
**Migrated By:** [Your name]  
**Initial Version:** v1.0.0
